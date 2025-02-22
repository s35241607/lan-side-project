using lan_side_project.Data;
using lan_side_project.Models;
using Microsoft.EntityFrameworkCore;

namespace lan_side_project.Repositories;

public class PermissionRepository(AppDbContext db) : GenericRepository<Role>(db)
{
    // 確認 Permission 是否存在
    public async Task<bool> IsPermissionExistsAsync(string permissionName)
    {
        return await db.Permissions.AnyAsync(permission => permission.Name == permissionName);
    }

}
