using lan_side_project.Models;
using Microsoft.EntityFrameworkCore;

namespace lan_side_project.Data
{
    public partial class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
    }
}
