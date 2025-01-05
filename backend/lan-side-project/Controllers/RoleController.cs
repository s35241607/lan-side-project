using lan_side_project.DTOs.Requests.Role;
using lan_side_project.DTOs.Responses;
using lan_side_project.DTOs.Responses.Role;
using lan_side_project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace lan_side_project.Controllers;

[ApiController]
[Route("api/v1/roles")]
[Authorize]
public class RoleController(RoleService roleService) : BaseController
{
    [HttpGet]
    public async Task<ActionResult<List<RoleResponse>>> GetAllAsync()
        => ErrorOrOk(await roleService.GetAllAsync());


    [HttpGet("{id}")]
    public async Task<ActionResult<RoleResponse>> GetByIdAsync(int id)
        => ErrorOrOk(await roleService.GetByIdAsync(id));

    [HttpPost]
    public async Task<ActionResult<RoleResponse>> CreateAsync(CreateRoleRequest createRoleRequest)
    {
        var result = await roleService.CreateAsync(createRoleRequest);
        return ErrorOrCreated(result, $"api/v1/roles/{result.Value?.Id}");
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<RoleResponse>> UpdateAsync(int id, UpdateRoleRequest updateRoleRequest)
        => ErrorOrOk(await roleService.UpdateAsync(updateRoleRequest));


    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse>> DeleteAsync(int id)
        => ErrorOrOk(await roleService.DeleteAsync(id));
}
