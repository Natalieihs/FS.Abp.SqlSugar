using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Auditing;

namespace Abp.SqlSugar
{
    public class SqlSugarAuditingStore : IAuditingStore
    {
        private readonly ISqlSugarClient _sqlSugarClient;
        public SqlSugarAuditingStore(ISqlSugarClient sqlSugarClient)
        {
            _sqlSugarClient = sqlSugarClient;
        }
        public async Task SaveAsync(AuditLogInfo auditInfo)
        {
            var auditLog = new AuditLog
            {
                Id = Guid.NewGuid(),
                UserId = auditInfo.UserId,
                HttpMethod = auditInfo.HttpMethod,
                Url = auditInfo.Url,
                ExecutionTime = auditInfo.ExecutionTime,
                Duration = auditInfo.ExecutionDuration,
                // 设置更多审计信息字段
            };
            await _sqlSugarClient.Insertable(auditLog).ExecuteCommandAsync();
        }
    }
}
