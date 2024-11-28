using lan_side_project.Data;
using lan_side_project.Models;
using lan_side_project.Utils;
using Microsoft.EntityFrameworkCore;

namespace lan_side_project.Repositories
{
    public class UserRepository(AppDbContext db)
    {
        // 根據 Username 查詢使用者
        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await db.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        // 根據 ID 查詢使用者
        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await db.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        // 新增使用者
        public async Task AddUserAsync(User user)
        {
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
        }

        // 更新使用者資料
        public async Task UpdateUserAsync(User user)
        {
            var existingUser = await db.Users.FindAsync(user.Id);

            if (existingUser != null)
            {
                MapperUtils.Mapper.Map(user, existingUser);
                db.Users.Update(existingUser);
                await db.SaveChangesAsync();
            }
        }

        // 刪除使用者
        public async Task DeleteUserAsync(int userId)
        {
            var user = await db.Users.FindAsync(userId);

            if (user == null)
                return;
            
            db.Users.Remove(user);
            await db.SaveChangesAsync();
            
        }

        // 確認使用者是否存在
        public async Task<bool> IsUserExistsAsync(string username)
        {
            return await db.Users.AnyAsync(u => u.Username == username);
        }

        // 取得所有使用者
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await db.Users
                .AsNoTracking()
                .ToListAsync();
        }

        // 分頁查詢使用者
        public async Task<List<User>> GetUsersWithPaginationAsync(int pageIndex, int pageSize)
        {
            return await db.Users
                .AsNoTracking()
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

    }
}
