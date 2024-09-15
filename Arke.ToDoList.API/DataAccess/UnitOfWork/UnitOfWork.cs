﻿using Arke.ToDoList.API.Context;
using System.Diagnostics.CodeAnalysis;

namespace Arke.ToDoList.API.DataAccess.UnitOfWork;

[ExcludeFromCodeCoverage]
public class UnitOfWork : IUnitOfWork
{
    private readonly DatabaseContext _dbContext;

    public UnitOfWork(DatabaseContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public Task<int> CommitAsync()
    {
        return _dbContext.SaveChangesAsync();
    }
}
