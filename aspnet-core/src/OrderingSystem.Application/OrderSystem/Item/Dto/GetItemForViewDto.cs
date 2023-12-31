using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.OrderSystem.Item.Dto
{
    public class GetItemForViewDto : Entity<long?>
    {
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public int DiscountId { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public int? TenantId { get; set; }
        public string LongDescription { get; set; }
        public string SortDescription { get; set; }
        public string ImgUrl { get; set; }
    }
}
