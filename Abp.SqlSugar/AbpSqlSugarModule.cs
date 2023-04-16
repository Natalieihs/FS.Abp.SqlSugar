using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Abp.SqlSugar
{

    [DependsOn(typeof(AbpDddDomainModule))]
    public class AbpSqlSugarModule: AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddOptions<SqlSugarOptions>();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var options = context.Services.ExecutePreConfiguredActions<SqlSugarOptions>();

            context.Services.AddScoped<ISqlSugarClient>(x =>
            {
                var db = new SqlSugarClient(options.ConnectionConfig);
                return db;
            });

            context.Services.AddAbpSqlSugar(options =>
            {
                options.ConnectionConfig = new ConnectionConfig
                {
                    ConnectionString = context.Services.GetConfiguration().GetConnectionString("Default"),
                    DbType = DbType.SqlServer,
                    IsAutoCloseConnection = true,
                    InitKeyType = InitKeyType.Attribute
                };
            });
        }
    }
}
