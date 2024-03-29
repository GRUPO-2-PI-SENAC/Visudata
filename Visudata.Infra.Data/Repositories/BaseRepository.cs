﻿using Microsoft.EntityFrameworkCore;
using PI.Domain.Interfaces;
using PI.Domain.Interfaces.Repositories;
using PI.Infra.Data.Context;
using System.Data.Common;

namespace PI.Infra.Data.Repositories;

public class BaseRepository<T> : IBaseRepository<T>  where T : class
{
    protected readonly VisudataDbContext _context;
    protected DbConnection _databaseConnection
    {
        get
        {
            return _context.Database.GetDbConnection();
        }
    }

    public BaseRepository(VisudataDbContext visudataDbContext)
    {
        _context = visudataDbContext;
    }

    public virtual async Task Add(T entity)
    {
        _context.Set<T>().Add(entity);
        _context.SaveChanges();
    }

    public virtual async Task Delete(int entityId)
    {
        _context.Set<T>().Remove(await GetById(entityId));
        _context.SaveChanges();
    }

    public virtual async Task<T> GetById(int entityId)
    {
        return await _context.Set<T>().FindAsync(entityId);
    }

    public virtual async Task<IEnumerable<T>> GetAll()
    {
        return _context.Set<T>().AsEnumerable();
    }

    public virtual async Task Update(T entity)
    {
        _context.Set<T>().Update(entity);
        _context.SaveChanges();
    }
}