using System.Linq.Expressions;
using Book.DataAccess.Data;
using Book.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Book.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> Set;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.Set = _db.Set<T>();
           // _db.Products.Include(u => u.Category).Include(u=>u.CategoryId);
        }
        public void Add(T entity)
        {
            Set.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool isTracked = false)
        {
            IQueryable<T> query;

            if (isTracked == true)
            {
                query = Set;
            } 
            else
            {
                query = Set.AsNoTracking();
            }

            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();

        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter, string? includeProperties=null)
        {
            IQueryable<T> query = Set;
           
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach(var includeProp in includeProperties
                    .Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }

        public void Remove(T entity)
        {
            Set.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            Set.RemoveRange(entities);
        }
    }
}
