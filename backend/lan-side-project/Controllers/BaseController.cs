using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lan_side_project.Controllers;
public abstract class BaseController : ControllerBase
{
    protected ActionResult<TValue> ErrorOrOkResponse<TValue>(ErrorOr<TValue> result)
    {
        ActionResult<TValue> response = Problem();
        result.SwitchFirst(
            value => { response = Ok(value); },
            error => {
                response = error.Type switch
                {
                    ErrorType.Conflict => Conflict(error),
                    ErrorType.NotFound => NotFound(error),
                    ErrorType.Validation => BadRequest(error),
                    ErrorType.Failure => UnprocessableEntity(error),
                    ErrorType.Unexpected => UnprocessableEntity(error),
                    _ => UnprocessableEntity(error)
                };
            });
        return response;
    }
    protected ActionResult<TValue> ErrorOrCreatedResponse<TValue>(ErrorOr<TValue> result, string uri)
    {
        ActionResult<TValue> response = Problem();
        result.SwitchFirst(
            value => { response = Created(uri, value); },
            error => {
                response = error.Type switch
                {
                    ErrorType.Conflict => Conflict(error),
                    ErrorType.NotFound => NotFound(error),
                    ErrorType.Validation => BadRequest(error),
                    ErrorType.Failure => UnprocessableEntity(error),
                    ErrorType.Unexpected => UnprocessableEntity(error),
                    _ => UnprocessableEntity(error)
                };
            });
        return response;
    }
    protected ActionResult<TValue> ErrorOrAcceptedResponse<TValue>(ErrorOr<TValue> result)
    {
        ActionResult<TValue> response = Problem();
        result.SwitchFirst(
            value => { response = Accepted(value); },
            error => {
                response = error.Type switch
                {
                    ErrorType.Conflict => Conflict(error),
                    ErrorType.NotFound => NotFound(error),
                    ErrorType.Validation => BadRequest(error),
                    ErrorType.Failure => UnprocessableEntity(error),
                    ErrorType.Unexpected => UnprocessableEntity(error),
                    _ => UnprocessableEntity(error)
                };
            });
        return response;
    }
    protected ActionResult ErrorOrNoContent(ErrorOr<object> result)
    {
        ActionResult response = Problem();
        result.SwitchFirst(
            _ => { response = NoContent(); },
            error => {
                response = error.Type switch
                {
                    ErrorType.Conflict => Conflict(error),
                    ErrorType.NotFound => NotFound(error),
                    ErrorType.Validation => BadRequest(error),
                    ErrorType.Failure => UnprocessableEntity(error),
                    ErrorType.Unexpected => UnprocessableEntity(error),
                    _ => UnprocessableEntity(error)
                };
            });
        return response;
    }
}