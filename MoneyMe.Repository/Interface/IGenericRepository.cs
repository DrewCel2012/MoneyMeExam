using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Query;
using MoneyMe.Model;
using System.Linq.Expressions;

namespace MoneyMe.Repository.Interface
{
    public interface IGenericRepository<TEntity> where TEntity : class, new()
    {
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null,
                                               Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                               params Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>[]? includes);
        
        Task<TEntity> GetByFilterAsync(Expression<Func<TEntity, bool>> filter,
                                       params Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>[]? includes);

        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Delete(int id);


        Task<ServiceResponse> ExecuteProcAsync(string sprocName, params SqlParameter[]? parameters);
    }
}
