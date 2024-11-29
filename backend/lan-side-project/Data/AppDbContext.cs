using lan_side_project.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace lan_side_project.Data;

public partial class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 使用 snake_case 命名規則
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            // 轉換表格名稱為 snake_case
            entity.SetTableName(ToSnakeCase(entity.GetTableName()!));

            // 轉換每個欄位名稱為 snake_case
            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(ToSnakeCase(property.Name));
            }
        }
    }

    // 轉換名稱為 snake_case
    private static string ToSnakeCase(string str)
    {
        return Regex.Replace(
            str,
            @"([a-z])([A-Z])",
            "$1_$2").ToLower();
    }
}
