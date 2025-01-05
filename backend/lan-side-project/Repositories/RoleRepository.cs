using lan_side_project.Data;
using lan_side_project.Models;
using Microsoft.EntityFrameworkCore;

namespace lan_side_project.Repositories;

public class RoleRepository(AppDbContext db) : GenericRepository<Role>(db)
{
    // 確認角色是否存在
    public async Task<bool> IsRoleExistsAsync(string roleName)
    {
        return await db.Roles.AnyAsync(role => role.Name == roleName);
    }

}
