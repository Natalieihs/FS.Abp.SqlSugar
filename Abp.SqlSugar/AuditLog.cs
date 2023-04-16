using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.SqlSugar
{
    public class AuditLog
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public string HttpMethod { get; set; }
        public string Url { get; set; }
        public DateTime ExecutionTime { get; set; }
        public int Duration { get; set; }
        // 更多审计信息字段
    }

}
