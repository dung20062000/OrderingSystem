using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.OrderSystem.Discount
{
    [Table("OdsDiscount")]
    public class OdsDiscount : FullAuditedEntity<long>, IEntity<long>
    {
        public string DiscountName { get; set; }
        public string DiscDescription { get; set;}
        public int UserId { get; set; }
    }
}
