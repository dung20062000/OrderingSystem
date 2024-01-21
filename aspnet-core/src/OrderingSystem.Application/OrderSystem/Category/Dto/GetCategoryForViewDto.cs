using Abp.Domain.Entities;

namespace OrderingSystem.OrderSystem.Category.Dto
{
    public class GetCategoryForViewDto : Entity<long?>
    {
        public string CategoryName { set; get; }
        public bool isDisplay { get; set; }
        public int? TenantId { get; set; }
    }
}
