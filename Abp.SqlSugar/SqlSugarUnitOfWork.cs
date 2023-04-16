using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Abp.SqlSugar
{
    public class SqlSugarUnitOfWork : UnitOfWorkBase
    {
        private readonly ISqlSugarClient _sqlSugarClient;

        public SqlSugarUnitOfWork(ISqlSugarClient sqlSugarClient, IUnitOfWorkDefaultOptions defaultOptions)
            : base(defaultOptions)
        {
            _sqlSugarClient = sqlSugarClient;
        }

        protected override Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            // 使用_sqlSugarClient执行保存更改的操作
        }

        protected override async Task CompleteAsync(CancellationToken cancellationToken)
        {
            // 使用_sqlSugarClient的事务处理功能提交或回滚事务
        }
    }

}
