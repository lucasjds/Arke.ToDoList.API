﻿using Arke.ToDoList.API.Data.Sql;
using Arke.ToDoList.API.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Arke.ToDoList.API.DataAccess.Repositories;

[ExcludeFromCodeCoverage]
public class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : class, IEntity
{
    private readonly Lazy<DbSet<TEntity>> _dbSet;
    private readonly DbContext _dbContext;

    public BaseRepository(DatabaseContext dbContext)
    {
        if (dbContext == null)
        {
            throw new ArgumentNullException(nameof(dbContext));
        }

        _dbSet = new Lazy<DbSet<TEntity>>(() => dbContext.Set<TEntity>());
        _dbContext = dbContext;
    }

    protected Lazy<DbSet<TEntity>> DbSet => _dbSet;

    public void Delete(TEntity entity)
    {
        DbSet.Value.Remove(entity);
    }

    public async Task<IEnumerable<TEntity>> FindAll()
    {
        return await DbSet.Value.ToListAsync();
    }

    public async Task<TEntity> FindById(Guid id)
    {
        return await DbSet.Value.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<TEntity> Save(TEntity entity)
    {
        var added = await DbSet.Value.AddAsync(entity);
        return added.Entity;
    }
}
