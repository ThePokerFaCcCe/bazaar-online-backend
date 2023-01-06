using BazaarOnline.Domain.Interfaces;
using BazaarOnline.Infra.Data.Contexts;

namespace BazaarOnline.Infra.Data.Repositories
{
    public class Repository : IRepository
    {
        private readonly BazaarDbContext _context;

        public Repository(BazaarDbContext context)
        {
            _context = context;
        }

        public TEntity Add<TEntity>(TEntity entity) where TEntity : class
        {
            return _context.Add<TEntity>(entity).Entity;
        }

        public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            _context.AddRange(entities);
        }

        public void Remove<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Remove<TEntity>(entity);
        }

        public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            _context.RemoveRange(entities);
        }

        public TEntity? Get<TEntity>(int id) where TEntity : class
        {
            return _context.Find<TEntity>(id);
        }
        
        public TEntity? Get<TEntity>(string id) where TEntity : class
        {
            return _context.Find<TEntity>(id);
        }

        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return _context.Set<TEntity>().AsQueryable();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Update<TEntity>(entity);
        }

        public void UpdateRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            _context.UpdateRange(entities);
        }
    }
}
