using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.OrderSystem.Item
{
    [Table("OdsItemCategory")]
    public class OdsItemCategory : FullAuditedEntity<long>, IEntity<long>
    {
        public int CategoryId { get; set; }
        public int ItemId { get; set; }
    }
}
