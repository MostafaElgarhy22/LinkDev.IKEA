using System.Linq.Expressions;
using LinkDev.IKEA.DAL.Common.Entities;
using LinkDev.IKEA.DAL.Contracts.Repositories;
using LinkDev.IKEA.DAL.Persistence.Common;
using LinkDev.IKEA.DAL.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.IKEA.DAL.Persistence.Repositories
{
    public class BaseRepository<TEntity, Tkey> : IBaseRepository<TEntity, Tkey>
        where TEntity : BaseEntity<Tkey>
        where Tkey : IEquatable<Tkey>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public TEntity? Get(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (includes is not null)
                query = includes(query);

            query = query.Where(filter);

            return query.FirstOrDefault();
        }

        public PeginatedResult<TEntity> GetAll(QueryParamaters paramaters, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? OrderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (includes is not null)
                query = includes(query);

            query = query.Where(filter);

            var totalCount = query.Count();

            if(OrderBy is not null)
                query = OrderBy(query);

            //Apply Pagination
            var entities = query
                .Skip(paramaters.PageSize * (paramaters.PageSize - 1))
                .Take(paramaters.PageSize)
                .ToList();

            return new PeginatedResult<TEntity>() { 
                Data = entities,
                TotalCount = totalCount,
                PageSize = paramaters.PageSize,
                PageIndex = paramaters.PageIndex };
        }

        public bool Exists(Expression<Func<TEntity, bool>> filter) => _dbSet.Any(filter);
      

        public TEntity? Get(int id) => _dbSet.Find(id); 


        public IEnumerable<TEntity> GetAll(bool withTracking = false)
        {
            if (!withTracking)
            {
                return _dbSet.AsNoTracking();
            }
            return _dbSet;
        }

        public void Add(TEntity entity) => _dbSet.Add(entity);


        public void Update(TEntity entity) => _dbSet.Update(entity);


        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

    }
}
