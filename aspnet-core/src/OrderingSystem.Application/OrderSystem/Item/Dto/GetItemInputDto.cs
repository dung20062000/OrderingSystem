using Abp.Application.Services.Dto;

namespace OrderingSystem.OrderSystem.Item.Dto
{
    public class GetItemInputDto : PagedAndSortedResultRequestDto 
    {
        public string filterText { get; set; }
    }
}
