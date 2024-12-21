using lan_side_project.Common;
using lan_side_project.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace lan_side_project.Data;

public partial class AppDbContext(DbContextOptions<AppDbContext> options, IUserContext userContext) : DbContext(options)
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<Permission> Permissions { get; set; } = null!;

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
        => Regex.Replace(str, @"([a-z])([A-Z])", "$1_$2").ToLower();

    public override int SaveChanges()
    {
        ApplyAuditInfo();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyAuditInfo();
        return await base.SaveChangesAsync(cancellationToken);
    }


    private void ApplyAuditInfo()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is AuditBase && (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entry in entries)
        {
            var auditEntity = (AuditBase)entry.Entity;

            // 檢查是否存在 CreatedAt 和 CreatedBy 欄位
            if (entry.State == EntityState.Added)
            {
                if (entry.Properties.Any(p => p.Metadata.Name == nameof(AuditBase.CreatedAt)))
                {
                    auditEntity.CreatedAt = DateTime.UtcNow;
                }
                if (entry.Properties.Any(p => p.Metadata.Name == nameof(AuditBase.CreatedBy)))
                {
                    auditEntity.CreatedBy = userContext.UserId;
                }
            }

            if (entry.State == EntityState.Modified)
            {
                if (entry.Properties.Any(p => p.Metadata.Name == nameof(AuditBase.UpdatedAt)))
                {
                    auditEntity.UpdatedAt = DateTime.UtcNow;
                }
                if (entry.Properties.Any(p => p.Metadata.Name == nameof(AuditBase.UpdatedBy)))
                {
                    auditEntity.UpdatedBy = userContext.UserId;
                }
            }
        }
    }
}
