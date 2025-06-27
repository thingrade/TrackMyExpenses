namespace TrackMyExpenses.API.Common.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync(); // Soft filter
        Task<IEnumerable<TEntity>> GetAllIncludingDeletedAsync(); // No filter
        Task<TEntity?> GetByIdAsync(Guid id);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task SoftDeleteAsync(TEntity entity);
        Task HardDeleteAsync(TEntity entity);
    }
}