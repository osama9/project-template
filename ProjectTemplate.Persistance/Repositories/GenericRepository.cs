using Microsoft.EntityFrameworkCore;
using ProjectTemplate.Core.Entities;
using ProjectTemplate.Core.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProjectTemplate.Persistance.Repositories
{
    public class GenericRepository
    {
        internal ApplicationDbContext _context;

        public GenericRepository() { }

        public GenericRepository(ApplicationDbContext context)
        {
            this._context = context;

        }

        public virtual List<TEntity> Create<TEntity>(List<TEntity> entites) where TEntity : class
        {


            var dbSet = _context.Set<TEntity>();

            foreach (var entity in entites)
            {
                if (entity is AuditableEntity)
                    (entity as AuditableEntity).InsertAudit();

                dbSet.Add(entity);
            }


            _context.SaveChanges();



            return entites;
        }

        public virtual TEntity Create<TEntity>(TEntity entity) where TEntity : class
        {


            var dbSet = _context.Set<TEntity>();

            if (entity is AuditableEntity)
                (entity as AuditableEntity).InsertAudit();


            dbSet.Add(entity);

            _context.SaveChanges();



            return entity;
        }

        public async virtual Task<List<TEntity>> CreateAsync<TEntity>(List<TEntity> entites) where TEntity : class
        {


            var dbSet = _context.Set<TEntity>();

            foreach (var entity in entites)
            {
                if (entity is AuditableEntity)
                    (entity as AuditableEntity).InsertAudit();
            }

            await dbSet.AddRangeAsync(entites);

            await _context.SaveChangesAsync();



            return entites;
        }

        public async virtual Task<TEntity> CreateAsync<TEntity>(TEntity entity) where TEntity : class
        {


            var dbSet = _context.Set<TEntity>();

            if (entity is AuditableEntity)
                (entity as AuditableEntity).InsertAudit();


            await dbSet.AddAsync(entity);

            await _context.SaveChangesAsync();



            return entity;
        }

        public virtual TEntity Update<TEntity>(TEntity entityToUpdate) where TEntity : class
        {


            var dbSet = _context.Set<TEntity>();

            if (_context.Entry(entityToUpdate).State == EntityState.Detached)
                dbSet.Attach(entityToUpdate);

            _context.Entry(entityToUpdate).State = EntityState.Modified;

            if (entityToUpdate is AuditableEntity)
                (entityToUpdate as AuditableEntity).UpdateAudit();

            _context.SaveChanges();



            return entityToUpdate;
        }

        public async virtual Task<TEntity> UpdateAsync<TEntity>(TEntity entityToUpdate) where TEntity : class
        {


            var dbSet = _context.Set<TEntity>();

            if (_context.Entry(entityToUpdate).State == EntityState.Detached)
                dbSet.Attach(entityToUpdate);

            _context.Entry(entityToUpdate).State = EntityState.Modified;

            if (entityToUpdate is AuditableEntity)
                (entityToUpdate as AuditableEntity).UpdateAudit();

            await _context.SaveChangesAsync();


            return entityToUpdate;
        }

        public virtual void Delete<TEntity>(TEntity entityToDelete) where TEntity : class
        {



            var dbSet = _context.Set<TEntity>();

            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }

            dbSet.Remove(entityToDelete);

            _context.SaveChanges();


        }

        public virtual void Delete<TEntity>(object id) where TEntity : class
        {


            var dbSet = _context.Set<TEntity>();

            var found = dbSet.Find(id);

            dbSet.Remove(found);

            _context.SaveChanges();


        }

        public async virtual Task DeleteAsync<TEntity>(object id) where TEntity : class
        {


            var dbSet = _context.Set<TEntity>();

            var found = await dbSet.FindAsync(id);

            dbSet.Remove(found);

            await _context.SaveChangesAsync();


        }

        public async virtual Task<int> CountAsync<TEntity>() where TEntity : BaseEntity
        {


            var dbSet = _context.Set<TEntity>();

            int count;

            count = await dbSet.CountAsync();

            return count;
        }

        public async virtual Task<int> CountAsync<TEntity>(SearchCriteria<TEntity> search) where TEntity : class
        {


            var dbSet = _context.Set<TEntity>();

            IQueryable<TEntity> query = dbSet;

            int count = 0;

            if (search.FilterExpression != null)
            {
                query = query.Where(search.FilterExpression);
            }

            count = await query.CountAsync();

            return count;
        }

        public virtual int Count<TEntity>() where TEntity : BaseEntity
        {


            var dbSet = _context.Set<TEntity>();

            int count;

            count = dbSet.Count();

            return count;
        }

        public virtual int Count<TEntity>(SearchCriteria<TEntity> search) where TEntity : class
        {


            var dbSet = _context.Set<TEntity>();

            IQueryable<TEntity> query = dbSet;

            int count = 0;

            if (search.FilterExpression != null)
            {
                query = query.Where(search.FilterExpression);
            }

            count = query.Count();

            return count;
        }

        public virtual SearchResult<TEntity> Search<TEntity>(SearchCriteria<TEntity> searchCriteria, params string[] includes) where TEntity : class
        {




            var dbSet = _context.Set<TEntity>();

            IQueryable<TEntity> query = dbSet;


            if (searchCriteria.FilterExpression != null)
            {
                query = query.Where(searchCriteria.FilterExpression);
            }

            foreach (var includeProperty in includes)
            {
                query = query.Include(includeProperty);
            }

            if (searchCriteria.SortExpression != null)
            {
                query = searchCriteria.SortExpression(query);
            }
            else
            {
                //query = query.OrderByDescending(entity => entity.ID);
            }

            SearchResult<TEntity> result = new SearchResult<TEntity>(searchCriteria)
            {
                TotalResultsCount = query.Count(),
            };

            query = query.Skip(searchCriteria.StartIndex).Take(searchCriteria.PageSize);

            result.Result = query.ToList();


            return result;

        }

        public async virtual Task<SearchResult<TEntity>> SearchAsync<TEntity>(SearchCriteria<TEntity> searchCriteria, params string[] includes) where TEntity : class
        {


            var dbSet = _context.Set<TEntity>();

            IQueryable<TEntity> query = dbSet;

            if (searchCriteria.FilterExpression != null)
            {
                query = query.Where(searchCriteria.FilterExpression);
            }

            foreach (var includeProperty in includes)
            {
                query = query.Include(includeProperty);
            }

            if (searchCriteria.SortExpression != null)
            {
                query = searchCriteria.SortExpression(query);
            }

            SearchResult<TEntity> result = new SearchResult<TEntity>(searchCriteria)
            {
                TotalResultsCount = await query.CountAsync(),
            };

            query = query.Skip(searchCriteria.StartIndex).Take(searchCriteria.PageSize);

            result.Result = await query.ToListAsync();


            return result;

        }

        public virtual TEntity GetByID<TEntity>(params object[] keys) where TEntity : BaseEntity
        {


            TEntity entity = _context.Set<TEntity>().Find(keys);

            return entity;

        }

        public async virtual Task<TEntity> GetByIDAsync<TEntity>(params object[] keys) where TEntity : BaseEntity
        {


            TEntity entity = await _context.Set<TEntity>().FindAsync(keys);

            return entity;

        }

        public virtual IEnumerable<TEntity> Get<TEntity>(
         Expression<Func<TEntity, bool>> filter = null,
         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
         string[] includeProperties = null, int? maxSize = null) where TEntity : class
        {


            var dbSet = _context.Set<TEntity>();

            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                if (maxSize.HasValue)
                    query = orderBy(query);
                else
                    query = orderBy(query);
            }
            else
            {
                if (maxSize.HasValue)
                    query = query.Take(maxSize.Value);
            }

            var result = query.ToList();

            return result;
        }
    }
}