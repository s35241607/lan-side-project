using lan_side_project.DTOs.Requests.Role;
using lan_side_project.DTOs.Responses.Role;
using lan_side_project.DTOs.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using lan_side_project.Services;
using lan_side_project.DTOs.Requests.Permission;
using lan_side_project.DTOs.Responses.Permission;

namespace lan_side_project.Controllers;

[ApiController]
[Route("api/v1/permissions")]
[Authorize]
public class PermissionController(PermissionService permissionService) : BaseController
{
    [HttpGet]
    public async Task<ActionResult<List<PermissionResponse>>> GetAllAsync()
        => ErrorOrOk(await permissionService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<PermissionResponse>> GetByIdAsync(int id)
        => ErrorOrOk(await permissionService.GetByIdAsync(id));

    [HttpPost]
    public async Task<ActionResult<PermissionResponse>> CreateAsync(CreatePermissionRequest createPermissionRequest)
    {
        var result = await permissionService.CreateAsync(createPermissionRequest);
        return ErrorOrCreated(result, $"api/v1/permissions/{result.Value?.Id}");
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PermissionResponse>> UpdateAsync(int id, UpdatePermissionRequest updatePermissionRequest)
        => ErrorOrOk(await permissionService.UpdateAsync(id, updatePermissionRequest));

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse>> DeleteAsync(int id)
        => ErrorOrOk(await permissionService.DeleteAsync(id));
}

