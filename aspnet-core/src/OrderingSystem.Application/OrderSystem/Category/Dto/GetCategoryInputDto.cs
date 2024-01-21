using Abp.Application.Services.Dto;

using System.Threading.Tasks;

namespace OrderingSystem.OrderSystem.Category.Dto
{
    public class GetCategoryInputDto : PagedAndSortedResultRequestDto
    {
        public string filterText { get; set; }
    }
}
