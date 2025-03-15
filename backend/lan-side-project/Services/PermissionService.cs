using ErrorOr;
using lan_side_project.DTOs.Requests.Role;
using lan_side_project.DTOs.Responses.Role;
using lan_side_project.DTOs.Responses;
using lan_side_project.Models;
using lan_side_project.Repositories;
using lan_side_project.Utils;
using lan_side_project.DTOs.Responses.Permission;
using lan_side_project.DTOs.Requests.Permission;

namespace lan_side_project.Services;

public class PermissionService(PermissionRepository permissionRepository)
{
    public async Task<ErrorOr<List<PermissionResponse>>> GetAllAsync()
    {
        var permissions = await permissionRepository.GetAllAsync();
        return MapperUtils.Mapper.Map<List<PermissionResponse>>(permissions);
    }

    public async Task<ErrorOr<PermissionResponse>> GetByIdAsync(int id)
    {
        var permission = await permissionRepository.GetByIdAsync(id);
        return MapperUtils.Mapper.Map<PermissionResponse>(permission);
    }

    public async Task<ErrorOr<PermissionResponse>> CreateAsync(CreatePermissionRequest createPermissionRequest)
    {
        // 確認角色名稱是否已存在
        if (await permissionRepository.IsPermissionExistsAsync(createPermissionRequest.Name))
        {
            return Error.Conflict("PermissionNameAlreadyExists", "A permission with the same name already exists.");
        }

        var permission = MapperUtils.Mapper.Map<Role>(createPermissionRequest);
        await permissionRepository.AddAsync(permission);
        return MapperUtils.Mapper.Map<PermissionResponse>(permission);
    }

    public async Task<ErrorOr<PermissionResponse>> UpdateAsync(int id, UpdatePermissionRequest updatePermissionRequest)
    {
        // 確認角色名稱是否已存在
        if (await permissionRepository.IsPermissionExistsAsync(updatePermissionRequest.Name))
        {
            return Error.Conflict("PermissionNameAlreadyExists", "A permission with the same name already exists.");
        }

        var permission = await permissionRepository.GetByIdAsync(id);

        // 確認角色ID是否存在
        if (permission is null)
            return Error.NotFound("PermissionIdIsNotExists", $"A permission id with {id} is not exists.");

        MapperUtils.Mapper.Map(permission, updatePermissionRequest);


        await permissionRepository.UpdateAsync(permission);
        return MapperUtils.Mapper.Map<PermissionResponse>(permission);
    }

    public async Task<ErrorOr<ApiResponse>> DeleteAsync(int id)
    {
        await permissionRepository.DeleteAsync(id);
        return ApiResponse.Success("Permission deleted successfully.");
    }
}
