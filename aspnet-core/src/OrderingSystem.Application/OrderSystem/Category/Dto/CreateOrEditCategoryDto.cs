using Abp.Application.Services.Dto;

namespace OrderingSystem.OrderSystem.Category.Dto
{
    public class CreateOrEditCategoryDto : EntityDto<long?>
    {
        public string CategoryName { set; get; }
        public bool isDisplay { get; set; }
    }
}
