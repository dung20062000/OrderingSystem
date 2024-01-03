using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using OrderingSystem.OrderSystem.Item.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.OrderSystem.Item
{
    public class ItemAppService : ApplicationService, IItemAppService
    {
        private readonly IRepository<OdsItem, long> _repositoryItem;
        public ItemAppService(IRepository<OdsItem, long> repositoryItem)
        {
            _repositoryItem = repositoryItem;
        }

        public async Task<PagedResultDto<GetItemForViewDto>> GetAll(GetItemInputDto input)
        {
            var teantId = AbpSession.TenantId;
            var querybase = from items in _repositoryItem.GetAll()
                            .Where(e => e.ItemName.Contains(input.filterText) || input.filterText == null || e.SortDescription.Contains(input.filterText))
                            select new GetItemForViewDto
                            {
                                Id = items.Id,
                                ItemName = items.ItemName,
                                SortDescription = items.SortDescription,
                                Price = items.Price,
                                LongDescription = items.LongDescription,
                                DiscountId = items.DiscountId,
                                CategoryId = items.CategoryId,
                                ImgUrl = items.ImgUrl,
                                TenantId = teantId,
                            };
            var totalCount = querybase.Count();
            var pageFilter = querybase.PageBy(input).ToListAsync();
            return new PagedResultDto<GetItemForViewDto>(totalCount, await pageFilter);
        }

        public async Task CreateOrEdit(CreateOrEditItemDto input)
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
        protected virtual async Task Create(CreateOrEditItemDto input)
        {
            var tenantId = AbpSession.TenantId;
            var itemCount = _repositoryItem.GetAll().Where(e => (long)e.Id == input.Id).Count();
            if (itemCount >= 1)
            {
                throw new UserFriendlyException(00, L("ThisItemAlreadyExists"));
            }
            else
            {
                var itemMapper = ObjectMapper.Map<OdsItem>(input);
                await _repositoryItem.InsertAsync(itemMapper);
            }
        }
        protected virtual async Task Update(CreateOrEditItemDto input)
        {
                var items = await _repositoryItem.FirstOrDefaultAsync((long)input.Id);
                ObjectMapper.Map(input, items);
        }

        public async Task DeleteItem(EntityDto<long> input)
        {
            var items = await _repositoryItem.GetAll().FirstOrDefaultAsync(e => e.Id == input.Id);
            await _repositoryItem.DeleteAsync(items.Id); 

        }
    }
}
