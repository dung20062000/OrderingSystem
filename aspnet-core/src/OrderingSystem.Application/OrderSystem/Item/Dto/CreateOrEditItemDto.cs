using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.OrderSystem.Item.Dto
{
    public class CreateOrEditItemDto : EntityDto<long?>
    {
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public int? DiscountId { get; set; }
        public List<int> CategoryIds { get; set; }
        public int UserId { get; set; }
        public int? TenantId { get; set; }
        public bool isDisplay { get; set; }
        public string LongDescription { get; set; }
        public string SortDescription { get; set; }
        public string ImgUrl { get; set; }
    }
}
