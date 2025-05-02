using System.Linq.Expressions;
using LinkDev.IKEA.DAL.Common.Entities;
using LinkDev.IKEA.DAL.Persistence.Common;

namespace LinkDev.IKEA.DAL.Contracts.Repositories
{
    public interface IBaseRepository<TEntity, TKey>
           where TEntity : BaseEntity<TKey>
           where TKey : IEquatable<TKey>
    {

        TEntity? Get(int id);

        TEntity? Get(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null);

        PeginatedResult<TEntity> GetAll(QueryParamaters paramaters, Expression<Func<TEntity, bool>> filter,Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? OrderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null);


        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(int id);

        bool Exists(Expression<Func<TEntity, bool>> filter);
    }
}
