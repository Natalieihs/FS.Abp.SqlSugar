using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.SqlSugar
{
    public static class AbpApplicationBuilderExtensions
    {
        public static void AddAbpSqlSugar(this IServiceCollection services, Action<SqlSugarOptions> configureAction)
        {
            services.Configure(configureAction);
        }
    }

}
