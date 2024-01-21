using Abp.Application.Services;
using Abp.Application.Services.Dto;
using OrderingSystem.OrderSystem.Category.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.OrderSystem.Category
{
    public interface ICategoryAppService : IApplicationService
    {
        Task<PagedResultDto<GetCategoryForViewDto>> GetAll(GetCategoryInputDto input);
        Task<List<GetCategoryForViewDto>> GetAllForCreateItem();
        Task CreateOrEdit(CreateOrEditCategoryDto input);
        Task Delete(EntityDto<long> input);


    }
}
