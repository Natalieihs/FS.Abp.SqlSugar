using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Linq;

namespace Abp.SqlSugar
{
    public class SqlSugarRepository<TEntity, TKey> : IRepository<TEntity, TKey>
     where TEntity : class, IEntity<TKey>, new()
    {
        private readonly ISqlSugarClient _sqlSugarClient;

        public SqlSugarRepository(ISqlSugarClient sqlSugarClient)
        {
            _sqlSugarClient = sqlSugarClient;
        }

        public IAsyncQueryableExecuter AsyncExecuter => throw new NotImplementedException();

        public Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            _sqlSugarClient.Deleteable<TEntity>(predicate).ExecuteCommandAsync(cancellationToken);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(TKey id, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            _sqlSugarClient.Deleteable<TEntity>().In(id).ExecuteCommandAsync(cancellationToken);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            _sqlSugarClient.Deleteable<TEntity>(entity).ExecuteCommandAsync(cancellationToken);
            return Task.CompletedTask;
        }

        public Task DeleteDirectAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            _sqlSugarClient.Deleteable<TEntity>().Where(predicate).ExecuteCommandAsync(cancellationToken);
            return Task.CompletedTask;
        }

        public Task DeleteManyAsync(IEnumerable<TKey> ids, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            _sqlSugarClient.Deleteable<TEntity>().In(ids).ExecuteCommandAsync(cancellationToken);
            return Task.CompletedTask;
        }

        public Task DeleteManyAsync(IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            _sqlSugarClient.Deleteable<TEntity>(entities).ExecuteCommandAsync();
            return Task.CompletedTask;
        }

        /// <summary>
        /// 查询单个记录
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            return _sqlSugarClient.Queryable<TEntity>().SingleAsync(predicate);
        }

        public Task<TEntity> FindAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            return _sqlSugarClient.Queryable<TEntity>().InSingleAsync(id);
        }

        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            return FindAsync(predicate);
        }

        public Task<TEntity> GetAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            return FindAsync(id);
        }

        public Task<long> GetCountAsync(CancellationToken cancellationToken = default)
        {
            var count = _sqlSugarClient.Queryable<TEntity>().CountAsync(cancellationToken);
            Task<long> convertedTask = count.ContinueWith(task =>
            {
                int intValue = task.Result;
                long longValue = (long)intValue;
                return Task.FromResult(longValue);
            }).Unwrap();
            return convertedTask;
        }

        public Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            return _sqlSugarClient.Queryable<TEntity>().Where(predicate).ToListAsync(cancellationToken);
        }

        public Task<List<TEntity>> GetListAsync(bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            return _sqlSugarClient.Queryable<TEntity>().ToListAsync(cancellationToken);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <param name="sorting"></param>
        /// <param name="includeDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<List<TEntity>> GetPagedListAsync(int skipCount, int maxResultCount, string sorting, bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            return _sqlSugarClient.Queryable<TEntity>().ToPageListAsync(skipCount, maxResultCount, cancellationToken);
        }

        public Task<IQueryable<TEntity>> GetQueryableAsync()
        {
            return ToIQueryableAsync(_sqlSugarClient.Queryable<TEntity>());

        }
        private async Task<IQueryable<TEntity>> ToIQueryableAsync(ISugarQueryable<TEntity> sugarQueryable)
        {
            var list = await sugarQueryable.ToListAsync();
            return list.AsQueryable();
        }

        public Task<TEntity> InsertAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            return _sqlSugarClient.Insertable<TEntity>(entity).ExecuteReturnEntityAsync();
        }

        public Task InsertManyAsync(IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            return _sqlSugarClient.Insertable<TEntity>(entities).ExecuteCommandAsync(cancellationToken);
        }

        public Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            _sqlSugarClient.Updateable<TEntity>(entity).ExecuteCommandAsync(cancellationToken);
            return Task.FromResult(entity);
        }

        public Task UpdateManyAsync(IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            _sqlSugarClient.Updateable<TEntity>(entities).ExecuteCommandAsync(cancellationToken);
            return Task.CompletedTask;
        }

        public IQueryable<TEntity> WithDetails()
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> WithDetails(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<TEntity>> WithDetailsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<TEntity>> WithDetailsAsync(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            throw new NotImplementedException();
        }

        // 实现IRepository<TEntity, TKey>接口的方法，使用_sqlSugarClient进行数据库操作
    }
}
