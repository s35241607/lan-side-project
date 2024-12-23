using ErrorOr;
using lan_side_project.DTOs.Responses;

namespace lan_side_project.Services;

public class UserImageService
{
    private readonly long _maxFileSize = 2 * 1024 * 1024;
    private readonly HashSet<string> _allowedExtensions = [".jpg", ".jpeg", ".png"];
    private readonly string _baseFileUrl = "./files";

    
    public UserImageService()
    {
        if (!Directory.Exists(_baseFileUrl))
        {
            Directory.CreateDirectory(_baseFileUrl);
        }
    }

    public async Task<ErrorOr<ApiResponse>> UploadAvatarAsync(int userId, IFormFile file)
    {

        // 驗證檔案大小
        if (file.Length > _maxFileSize)
        {
            return Error.Validation("FileTooLarge", "The file size exceeds the maximum allowed size of 2MB.");
        }

        // 驗證檔案類型
        var fileExtension = Path.GetExtension(file.FileName).ToLower();
        if (!_allowedExtensions.Contains(fileExtension))
        {
            return Error.Validation("FileInvalidType", "Invalid file type. Only .jpg, .jpeg, and .png are allowed.");
        }

        // 創建使用者資料夾 (如果不存在的話)
        var userFolderPath = Path.Combine(_baseFileUrl, "users", userId.ToString());
        if (!Directory.Exists(userFolderPath))
        {
            Directory.CreateDirectory(userFolderPath);
        }

        // 設定檔案儲存的路徑 (儲存為 avatar.png)
        var filePath = Path.Combine(userFolderPath, "avatar.png");
        
        // 儲存檔案
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        return ApiResponse.Success("User avatar uploaded successfully.");
    }

    public async Task<ErrorOr<byte[]>> GetAvatarAsync(int userId)
    {
        // 設定使用者頭像的路徑
        var userFolderPath = Path.Combine(_baseFileUrl, "users", userId.ToString());
        var filePath = Path.Combine(userFolderPath, "avatar.png");

        // 檢查檔案是否存在
        if (!File.Exists(filePath))
        {
            return Error.NotFound("UserAvatarNotFound", "The avatar for the user could not be found.");
        }

        // 讀取檔案並回傳
        var fileBytes = await File.ReadAllBytesAsync(filePath);
        return fileBytes;
    }
}

