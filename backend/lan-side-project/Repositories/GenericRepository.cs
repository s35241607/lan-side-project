﻿using lan_side_project.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;

namespace lan_side_project.Repositories;

public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly AppDbContext _dbContext;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IQueryable<T>>? include = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        int? skip = null,
        int? take = null,
        bool asNoTracking = false)
    {
        IQueryable<T> query = _dbSet;

        // 應用過濾條件
        if (filter != null)
        {
            query = query.Where(filter);
        }

        // 應用相關資料 Include
        if (include != null)
        {
            query = include(query);
        }

        // 應用排序
        if (orderBy != null)
        {
            query = orderBy(query);
        }

        // 應用分頁
        if (skip.HasValue)
        {
            query = query.Skip(skip.Value);
        }
        if (take.HasValue)
        {
            query = query.Take(take.Value);
        }

        // 是否使用 NoTracking
        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }

        return await query.ToListAsync();
    }

    /// <summary>
    /// 取得所有資料
    /// </summary>
    /// <returns></returns>
    public virtual async Task<List<T>> GetAllAsync()
    {
        return await _dbSet
            .AsNoTracking()
            .ToListAsync();
    }

    /// <summary>
    /// 根據 ID 查詢
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
    }

    /// <summary>
    /// 新增一筆資料
    /// </summary>
    /// <returns></returns>
    /// <param name="entity"></param>
    public virtual async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// 更新資料
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual async Task UpdateAsync(T entity)
    {
        var entityId = (int)typeof(T).GetProperty("Id")!.GetValue(entity)!;
        var existingEntity = await _dbSet.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == entityId);

        if (existingEntity != null)
        {
            _dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _dbContext.SaveChangesAsync();
        }
    }

    /// <summary>
    /// 刪除資料
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual async Task DeleteAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);

        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
