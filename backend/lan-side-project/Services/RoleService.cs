using ErrorOr;
using lan_side_project.DTOs.Requests.Role;
using lan_side_project.DTOs.Responses;
using lan_side_project.DTOs.Responses.Role;
using lan_side_project.Models;
using lan_side_project.Repositories;
using lan_side_project.Utils;
using System.Reflection.Metadata.Ecma335;

namespace lan_side_project.Services;

public class RoleService(RoleRepository roleRepository)
{
    public async Task<ErrorOr<List<RoleResponse>>> GetAllAsync()
    {
        var roles = await roleRepository.GetAllAsync();
        return MapperUtils.Mapper.Map<List<RoleResponse>>(roles);
    }

    public async Task<ErrorOr<RoleResponse>> GetByIdAsync(int id)
    {
        var role = await roleRepository.GetByIdAsync(id);
        return MapperUtils.Mapper.Map<RoleResponse>(role);
    }

    public async Task<ErrorOr<RoleResponse>> CreateAsync(CreateRoleRequest createRoleRequest)
    {
        // 確認角色名稱是否已存在
        if (await roleRepository.IsRoleExistsAsync(createRoleRequest.Name))
        {
            return Error.Conflict("RoleNameAlreadyExists", "A role with the same name already exists.");
        }

        var role = MapperUtils.Mapper.Map<Role>(createRoleRequest);
        await roleRepository.AddAsync(role);
        return MapperUtils.Mapper.Map<RoleResponse>(role);
    }

    public async Task<ErrorOr<RoleResponse>> UpdateAsync(int id, UpdateRoleRequest updateRoleRequest)
    {
        // 確認角色名稱是否已存在
        if (await roleRepository.IsRoleExistsAsync(updateRoleRequest.Name))
        {
            return Error.Conflict("RoleNameAlreadyExists", "A role with the same name already exists.");
        }

        var role = await roleRepository.GetByIdAsync(id);

        // 確認角色ID是否存在
        if (role is null)
            return Error.NotFound("RoleIdIsNotExists", $"A role id with {id} is not exists.");

        MapperUtils.Mapper.Map(role, updateRoleRequest);


        await roleRepository.UpdateAsync(role);
        return MapperUtils.Mapper.Map<RoleResponse>(role);
    }

    public async Task<ErrorOr<ApiResponse>> DeleteAsync(int id)
    {
        await roleRepository.DeleteAsync(id);
        return ApiResponse.Success("Role deleted successfully.");
    }
}
