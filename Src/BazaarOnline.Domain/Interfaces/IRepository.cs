namespace BazaarOnline.Domain.Interfaces
{
    public interface IRepository
    {
        IQueryable<TEntity> GetAll<TEntity>() where TEntity : class;

        TEntity? Get<TEntity>(int id) where TEntity : class;

        TEntity? Get<TEntity>(string id) where TEntity : class;

        TEntity? Get<TEntity>(Guid id) where TEntity : class;

        TEntity Add<TEntity>(TEntity entity) where TEntity : class;

        void Update<TEntity>(TEntity entity) where TEntity : class;

        void Remove<TEntity>(TEntity entity) where TEntity : class;

        void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        void UpdateRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        void Save();
    }
}