using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Serilog.Extensions.Hosting;
using System.Net;
using System.Security.Authentication;

namespace lan_side_project.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IConfiguration config, DiagnosticContext diagnosticContext)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            // 啟用請求體的緩衝，以便可以多次讀取
            httpContext.Request.EnableBuffering();
            await next(httpContext);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred.");

            await HandleExceptionAsync(httpContext, ex);

            diagnosticContext.SetException(ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var errorResponse = new
        {
            code = "INTERNAL_SERVER_ERROR",
            message = "An unexpected error occurred. Please try again later.",
            details = exception?.Message ?? "No further details available."
        };

        // 記錄錯誤訊息
        context.Response.Headers.Append("X-Request-Id", context.TraceIdentifier);

        // 使用 WriteAsJsonAsync 來正確寫入錯誤訊息
        await context.Response.WriteAsJsonAsync(errorResponse);
    }
}