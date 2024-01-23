using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.OrderSystem.Discount.Dto
{
    public class GetDiscountForViewDto : Entity<long?>
    {
        public string DiscountName { get; set; }
        public string DiscDescription { get; set; }
    }
}
