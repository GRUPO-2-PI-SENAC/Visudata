using PI.Domain.Interfaces;
using PI.Domain.Interfaces.Repositories;
using PI.Infra.Data.Context;

namespace PI.Infra.Data.Repositories;

public class BaseRepository<T> : IBaseRepository<T>  where T : class
{
    protected readonly VisudataDbContext _context;

    public BaseRepository(VisudataDbContext visudataDbContext)
    {
        _context = visudataDbContext;
    }

    public async Task Add(T entity)
    {
        _context.Set<T>().Add(entity);
        _context.SaveChanges();
    }

    public async Task RemoveById(int entityId)
    {
        _context.Set<T>().Remove(await GetById(entityId));
        _context.SaveChanges();
    }

    public async Task<T> GetById(int entityId)
    {
        return await _context.Set<T>().FindAsync(entityId);
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return _context.Set<T>().AsEnumerable();
    }

    public async Task Update(T entity)
    {
        _context.Set<T>().Update(entity);
        _context.SaveChanges();
    }
}