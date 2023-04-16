using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace Abp.SqlSugar
{
    public static class AbpSqlSugarRepositoryExtensions
    {
        public static void AddSqlSugarRepository<TDbContext, TEntity, TKey>(this IServiceCollection services)
            where TDbContext : IUnitOfWork
            where TEntity : class, IEntity<TKey>,new ()
        {
            services.AddScoped<IRepository<TEntity, TKey>>(x =>
            {
                var sqlSugarClient = x.GetRequiredService<ISqlSugarClient>();
                return new SqlSugarRepository<TEntity, TKey>(sqlSugarClient);
            });
        }
    }

}
