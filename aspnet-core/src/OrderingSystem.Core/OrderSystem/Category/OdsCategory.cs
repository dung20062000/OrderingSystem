using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderingSystem.OrderSystem.Category
{
    [Table("OdsCategory")]
    public class OdsCategory : FullAuditedEntity<long>, IEntity<long>
    {
        public string CategoryName { set; get; }
        public bool isDisplay { get; set; }
        public int? TenantId { get; set; }

    }
}
