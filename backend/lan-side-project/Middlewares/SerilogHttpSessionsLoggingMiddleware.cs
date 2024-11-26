using Serilog;
using System.Text.Json.Nodes;
using System.Text.Json;

namespace lan_side_project.Middlewares
{
    [Flags]
    public enum HttpSessionInfoToLog
    {
        All = -1,
        None = 0,
        QueryString = 1,
        Request = RequestHeaders | RequestBody | QueryString,
        RequestHeaders = 64,
        RequestBody = 128,
        Response = ResponseHeaders | ResponseBody,
        ResponseHeaders = 2048,
        ResponseBody = 4096,
    }

    public static class SerilogHttpSessionsLoggingMiddleware
    {
        private const int MINIMUM_RESPONSE_BODY_HTTP_STATUS = 400;
        /// <summary>
        /// Allows to log information about the http sessions processed.
        /// </summary>
        /// <param name="app">Application builder istance.</param>
        /// <param name="settings">Enum flags that defines what extra information should be logged.</param>
        /// <example>
        /// public class Startup
        /// {
        ///   public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        ///   {
        ///     app.UseHttpsRedirection();
        ///     app.UseStaticFiles();
        ///     app.UseSerilogHttpSessionsLogging(HttpSessionInfoToLog.All);
        ///     ...
        ///   }
        /// }
        /// </example>
        public static void UseSerilogHttpSessionsLogging(this IApplicationBuilder app, HttpSessionInfoToLog settings = HttpSessionInfoToLog.None)
        {
            if (settings.HasFlag(HttpSessionInfoToLog.RequestBody) || settings.HasFlag(HttpSessionInfoToLog.ResponseBody))
                app.Use(async (context, next) =>
                {
                    if (settings.HasFlag(HttpSessionInfoToLog.RequestBody))
                        context.Request.EnableBuffering();

                    if (settings.HasFlag(HttpSessionInfoToLog.ResponseBody))
                    {
                        var originalRespBody = context.Response.Body;
                        using var newResponseBody = new MemoryStream();
                        context.Response.Body = newResponseBody;    //to capture it's value in-flight.
                        await next.Invoke();
                        newResponseBody.Position = 0;
                        await newResponseBody.CopyToAsync(originalRespBody);
                        context.Response.Body = originalRespBody;
                    }
                });

            app.UseSerilogRequestLogging(options =>
            {
                // 如果要自訂訊息的範本格式，可以修改這裡，但修改後並不會影響結構化記錄的屬性
                options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} {UserID} responded {StatusCode} in {Elapsed:0.0000} ms";

                // 你可以從 httpContext 取得 HttpContext 下所有可以取得的資訊！
                options.EnrichDiagnosticContext = async (diagnosticContext, httpContext)
                => await LogEnrichment(diagnosticContext, httpContext, settings);

            });
        }

        static async Task LogEnrichment(IDiagnosticContext diagnosticContext, HttpContext httpContext, HttpSessionInfoToLog settings)
        {
            try
            {
                diagnosticContext.Set("RequestId", httpContext.TraceIdentifier);
                diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
                diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
                diagnosticContext.Set("UserID", httpContext.User.Identity?.Name);
                diagnosticContext.Set("ClientIP", httpContext.Connection.RemoteIpAddress?.ToString());
                diagnosticContext.Set("UserAgent", httpContext.Request.Headers["User-Agent"].FirstOrDefault());

                const string headersSeparator = ", ";
                if (settings.HasFlag(HttpSessionInfoToLog.QueryString))
                    diagnosticContext.Set("QueryString", httpContext.Request.QueryString);

                if (settings.HasFlag(HttpSessionInfoToLog.RequestHeaders))
                    diagnosticContext.Set("RequestHeaders", string.Join(headersSeparator, httpContext.Request.Headers));

                if (settings.HasFlag(HttpSessionInfoToLog.ResponseHeaders))
                    diagnosticContext.Set("ResponseHeaders", string.Join(headersSeparator, httpContext.Response.Headers));

                if (settings.HasFlag(HttpSessionInfoToLog.RequestBody))
                {
                    httpContext.Request.EnableBuffering();
                    var rawBody = await ReadStream(httpContext.Request.Body);
                    // 移除敏感資訊
                    var sanitizedBody = MaskSensitiveData(rawBody, "password", "******");
                    diagnosticContext.Set("RequestBody", sanitizedBody, false);
                }

                if (settings.HasFlag(HttpSessionInfoToLog.ResponseBody) && httpContext.Response.StatusCode >= MINIMUM_RESPONSE_BODY_HTTP_STATUS)
                    diagnosticContext.Set("ResponseBody", await ReadStream(httpContext.Response.Body), false);
            }
            catch (Exception ex)
            {
                diagnosticContext.SetException(ex);
            }
        }

        static async Task<string> ReadStream(Stream stream)
        {
            stream.Position = 0;
            using var reader = new StreamReader(stream, leaveOpen: true);
            var requestBodyText = await reader.ReadToEndAsync();
            stream.Position = 0;
            return requestBodyText;
        }

        private static string MaskSensitiveData(string rawBody, string sensitiveKey, string maskValue)
        {
            try
            {
                if (string.IsNullOrEmpty(rawBody)) return rawBody;

                // 使用 JsonNode 解析原始 JSON
                var jsonObject = JsonNode.Parse(rawBody);

                if (jsonObject == null) return rawBody;

                // 替換敏感數據
                ReplaceSensitiveValue(jsonObject, sensitiveKey.ToLower(), maskValue);

                // 序列化為 JSON
                return jsonObject.ToJsonString(new JsonSerializerOptions
                {
                    WriteIndented = false,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });
            }
            catch
            {
                return rawBody; // 無法解析時返回原始內容
            }
        }

        // 遞歸替換敏感資料
        private static void ReplaceSensitiveValue(JsonNode node, string sensitiveKey, string maskValue)
        {
            if (node is JsonObject obj)
            {
                foreach (var (key, _) in obj.ToList())
                {
                    if (key == sensitiveKey)
                    {
                        obj[key] = maskValue; // 修改該鍵的值
                    }
                    else if (obj[key] is JsonNode childNode)
                    {
                        ReplaceSensitiveValue(childNode, sensitiveKey, maskValue); // 遞歸處理子節點
                    }
                }
            }
        }
    }
}
