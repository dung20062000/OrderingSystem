using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using OrderingSystem.OrderSystem.Category.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.OrderSystem.Category
{
    public class CategoryAppService :  ApplicationService, ICategoryAppService
    {
        private readonly IRepository<OdsCategory, long> _repositoryCategory;

        public CategoryAppService(IRepository<OdsCategory, long> repositoryCategory)
        {
            _repositoryCategory = repositoryCategory;
        }

        public async Task<PagedResultDto<GetCategoryForViewDto>> GetAll(GetCategoryInputDto input)
        {
            var teantId = AbpSession.TenantId;
            var querybase = from categorys in _repositoryCategory.GetAll()
                            .Where(e => e.CategoryName.Contains(input.filterText) || input.filterText == null
                            )
                            select new GetCategoryForViewDto
                            {
                                CategoryName = categorys.CategoryName,
                                isDisplay = categorys.isDisplay,
                                TenantId = teantId,
                            };
            var totalCount = querybase.Count();
            var pageFilter = querybase.PageBy(input).ToListAsync();

            return new PagedResultDto<GetCategoryForViewDto>(totalCount, await pageFilter);
        }

        public async Task<List<GetCategoryForViewDto>> GetAllForCreateItem()
        {
            //var teantId = AbpSession.TenantId;

            var query = from categorys in _repositoryCategory.GetAll().Where(e => e.isDisplay == true && e.IsDeleted == false)
                        select new GetCategoryForViewDto
                        {
                            Id= categorys.Id,
                            CategoryName = categorys.CategoryName,
                            isDisplay = categorys.isDisplay,
                            //TenantId = teantId,
                        };

            return await query.ToListAsync();
        }
        public async Task CreateOrEdit(CreateOrEditCategoryDto input)
        {
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }
        protected virtual async Task Create(CreateOrEditCategoryDto input)
        {
            var teantId = AbpSession.TenantId;
            var categoryCount = _repositoryCategory.GetAll().Where(e => e.Id == input.Id).Count();
            if(categoryCount >= 1 )
            {
                throw new UserFriendlyException(00, L("ThisItemAlreadyExists"));
            }
            else
            {
                var cateMap = ObjectMapper.Map<OdsCategory>(input);
                await _repositoryCategory.InsertAsync(cateMap);
            }
        }
        protected virtual async Task Update(CreateOrEditCategoryDto input)
        {
            var teantId = AbpSession.TenantId;
            var category = await _repositoryCategory.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, category);
        }

        public async Task Delete(EntityDto<long> input)
        {
            var cateDelete = await _repositoryCategory.GetAll().FirstOrDefaultAsync(e => e.Id == input.Id);
            await _repositoryCategory.DeleteAsync(cateDelete.Id);
        }
    }
}
