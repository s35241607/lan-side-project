using ErrorOr;
using lan_side_project.DTOs.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Newtonsoft.Json.Linq;
using Error = ErrorOr.Error;

namespace lan_side_project.Controllers;
public abstract class BaseController : ControllerBase
{
    protected ActionResult<TValue> ErrorOrOk<TValue>(ErrorOr<TValue> result)
    {
        ActionResult<TValue> response = Problem();
        result.SwitchFirst(
            value => { response = Ok(value); },
            error => { response = HandleError(error); }
        );
        return response;
    }
    protected ActionResult<TValue> ErrorOrCreated<TValue>(ErrorOr<TValue> result, string uri)
    {
        ActionResult<TValue> response = Problem();
        result.SwitchFirst(
            value => { response = Created(uri, value); },
            error => { response = HandleError(error); }
        );
        return response;
    }
    protected ActionResult<TValue> ErrorOrAccepted<TValue>(ErrorOr<TValue> result)
    {
        ActionResult<TValue> response = Problem();
        result.SwitchFirst(
            value => { response = Accepted(value); },
            error => { response = HandleError(error); }
        );
        return response;
    }
    protected ActionResult ErrorOrNoContent(ErrorOr<object> result)
    {
        ActionResult response = Problem();
        result.SwitchFirst(
            _ => { response = NoContent(); },
            error => { response = HandleError(error); }
        );
        return response;
    }

    protected IActionResult ErrorOrFile<TValue>(ErrorOr<TValue> result, string contentType)
    {
        ActionResult response = Problem();
        result.SwitchFirst(
            value => { response = File(result.Value as byte[], contentType); },
            error => { response = HandleError(error); }
        );
        return response;
    }

    // 處理錯誤邏輯
    private ActionResult HandleError(Error error)
    {
        var errorResponse = ApiResponse.Error(error.Code, error.Description, error.Metadata);
        return error.Type switch
        {
            ErrorType.Conflict => Conflict(errorResponse),
            ErrorType.NotFound => NotFound(errorResponse),
            ErrorType.Validation => BadRequest(errorResponse),
            ErrorType.Failure => UnprocessableEntity(errorResponse),
            ErrorType.Unexpected => UnprocessableEntity(errorResponse),
            _ => UnprocessableEntity(errorResponse)
        };
    }
}