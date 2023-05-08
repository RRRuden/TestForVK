namespace Domain.Interfaces
{
    public interface IRepository<T>
    {
        Task<T?> GetByIdAsync(Guid id);
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
