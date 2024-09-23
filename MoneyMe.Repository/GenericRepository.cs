using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MoneyMe.Common.Helpers;
using MoneyMe.Model;
using MoneyMe.Repository.Entities;
using MoneyMe.Repository.Interface;
using System.Data;
using System.Linq.Expressions;

namespace MoneyMe.Repository
{
    public sealed class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, new()
    {
        private readonly MoneyMeDbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        private readonly string _connectionString;

        public GenericRepository(MoneyMeDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
            _connectionString = ConfigurationHelper.GetConfigurationValue<string>("ConnectionStrings:DefaultConnection");
        }


        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null,
                                                            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                                            params Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>[]? includes)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter is not null)
            {
                query = query.Where(filter);
            }

            if (includes is not null)
            {
                foreach (var includableQueryable in includes)
                {
                    query = includableQueryable(query);
                }
            }

            if (orderBy is not null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public async Task<TEntity> GetByFilterAsync(Expression<Func<TEntity, bool>> filter,
                                                    params Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>[]? includes)
        {
            IQueryable<TEntity> query = _dbSet;

            if (includes is not null)
            {
                foreach (var includableQueryable in includes)
                {
                    query = includableQueryable(query);
                }
            }

            return await query.FirstOrDefaultAsync(filter) ?? new TEntity();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            TEntity entity = _dbSet.Find(id);
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public async Task<ServiceResponse> ExecuteProcAsync(string sprocName, params SqlParameter[]? parameters)
        {
            ServiceResponse response = new() { DateTime = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"), HasError = false };

            using (SqlConnection connection = new(_connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = sprocName;
                command.CommandTimeout = 0;

                command.Parameters.Clear();
                if (parameters is not null && parameters.Length > 0)
                    command.Parameters.AddRange(parameters);

                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    await command.ExecuteNonQueryAsync();

                    SqlParameter? _param = parameters?.FirstOrDefault(p => p.Direction == ParameterDirection.Output);
                    if (_param is not null)
                    {
                        dynamic _output = command.Parameters[_param.ParameterName].Value;
                        response.Output = _output;
                    }

                    response.Message = $"Successfully executed stored procedure {sprocName}";
                }
                catch (Exception ex)
                {
                    response.HasError = true;
                    response.Message = ex.Message;
                    LogHelper.LogException(ex);
                }
                finally
                {
                    connection.Close();
                }
            }

            return response;
        }
    }
}
