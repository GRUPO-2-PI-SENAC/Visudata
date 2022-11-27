namespace PI.Domain.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task Add(T entity);
        Task RemoveById(int entityId);
        Task<T> GetById(int entityId);
        Task<IEnumerable<T>> GetAll();
        Task Update(T entity);
    }
}
