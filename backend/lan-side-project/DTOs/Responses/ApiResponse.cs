namespace lan_side_project.DTOs.Responses;

public class ApiResponse(string code, string message, Dictionary<string, object>? errors = null)
{
    public string Code { get; set; } = code;
    public string Message { get; set; } = message;
    public Dictionary<string, object>? Errors { get; set; } = errors;

    // 靜態方法：成功回應
    public static ApiResponse Success(string message)
    {
        return new ApiResponse("Success", message);
    }

    // 靜態方法：錯誤回應
    public static ApiResponse Error(string code, string message)
    {
        return new ApiResponse(code, message);
    }
    public static ApiResponse Error(string code, string message, Dictionary<string, object>? errors)
    {
        return new ApiResponse(code, message, errors);
    }
}