using Serilog.Extensions.Hosting;
using System.Net;

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
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;


        // 將 request Id 加入 HTTP 回應的標頭中
        context.Response.Headers.Append("X-Request-Id", context.TraceIdentifier);

        var errorResponse = new
        {
            message = "An unexpected error occurred. Please try again later.",
            details = exception.Message
        };

        await context.Response.WriteAsJsonAsync(errorResponse);
    }
}
