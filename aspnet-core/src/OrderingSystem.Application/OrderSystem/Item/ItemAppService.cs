using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using OrderingSystem.OrderSystem.Category;
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
        private readonly IRepository<OdsCategory, long> _repositoryCategory;
        private readonly IRepository<OdsItemCategory, long> _repositoryItemCategory;

        public ItemAppService(
            IRepository<OdsItem, long> repositoryItem,
            IRepository<OdsCategory, long> repositoryCategory,
            IRepository<OdsItemCategory, long> repositoryItemCategory)
        {
            _repositoryItem = repositoryItem;
            _repositoryCategory = repositoryCategory;
            _repositoryItemCategory = repositoryItemCategory;
        }

        public async Task<PagedResultDto<GetItemForViewDto>> GetAll(GetItemInputDto input)
        {
            var teantId = AbpSession.TenantId;
            var querybase = from items in _repositoryItemCategory.GetAll()

                            join dish in _repositoryItem.GetAll()
                            .Where(e => e.ItemName.Contains(input.filterText) || input.filterText == null || e.SortDescription.Contains(input.filterText))
                            on items.ItemId equals dish.Id

                            join categories in _repositoryCategory.GetAll()
                            on items.CategoryId equals categories.Id

                            group new { items, dish, categories }
                            by new { dish.ItemName, dish.Id,  dish.Price} 
                            into grouped

                            select new GetItemForViewDto
                            {
                                Id = grouped.Key.Id,
                                ItemName = grouped.Key.ItemName,
                                Price = grouped.Key.Price,
                                CategoryName = string.Join(", ", grouped.Select(e => e.categories.CategoryName)),
                                DiscountId = grouped.Select(x => x.dish.DiscountId).FirstOrDefault(),
                                TenantId = teantId,
                                SortDescription = grouped.Select(x => x.dish.SortDescription).FirstOrDefault(),
                                LongDescription = grouped.Select(x => x.dish.LongDescription).FirstOrDefault(),
                                ImgUrl = grouped.Select(x => x.dish.ImgUrl).FirstOrDefault(),
                                isDisplay = grouped.Select(x => x.dish.isDisplay).FirstOrDefault(),
                            };


            var totalCount = querybase.ToList().Count();
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
                var itemId = await _repositoryItem.InsertAndGetIdAsync(itemMapper);

                var categoryIds = input.CategoryIds ?? new List<int>();
                // Tạo các đối tượng OdsItemCategory và thêm vào bảng trung gian
                foreach (var categoryId in categoryIds)
                {
                    var itemCategory = new OdsItemCategory
                    {
                        ItemId = (int)itemId, // Id của item vừa thêm vào
                        CategoryId = categoryId
                    };

                    await _repositoryItemCategory.InsertAsync(itemCategory);
                }

            }
        }
        protected virtual async Task Update(CreateOrEditItemDto input)
        {
            var existingItem = await _repositoryItem.FirstOrDefaultAsync((long)input.Id);
            if (existingItem == null)
            {
                throw new UserFriendlyException(00, L("ThisItemNotFound"));
            }
            var duplicateItemCount = _repositoryItem.GetAll()
                    .Where(e => e.Id != input.Id && e.ItemName == input.ItemName).Count();
            if(duplicateItemCount >= 1)
            {
                throw new UserFriendlyException(00, L("DuplicateItemName"));
            }

            ObjectMapper.Map(input, existingItem);              // Map the input data to the existing item
            await _repositoryItem.UpdateAsync(existingItem);    // Update the existing item in the database

            var categoryIds = input.CategoryIds ?? new List<int>();
            var exictingItemCategories = await _repositoryItemCategory.GetAllListAsync(c => c.ItemId == existingItem.Id);
            var categoriesToRemove = exictingItemCategories.Where(d => !categoryIds.Contains(d.CategoryId)).ToList();

            foreach (var categoryToRemove in categoriesToRemove)
            {
                await _repositoryItemCategory.DeleteAsync(categoryToRemove);
            }

            // Add new associations
            foreach (var categoryId in categoryIds)
            {
                var existingItemCategory = exictingItemCategories.FirstOrDefault(e => e.CategoryId == categoryId);

                if(existingItemCategory == null)
                {
                    // If the association does not exist, create a new one
                    var newItemCategory = new OdsItemCategory
                    {
                        ItemId = (int)existingItem.Id,
                        CategoryId = categoryId,

                    };
                    await _repositoryItemCategory.InsertAsync(newItemCategory);
                }
            }


            //var items = await _repositoryItem.FirstOrDefaultAsync((long)input.Id);

        }

        public async Task DeleteItem(EntityDto<long> input)
        {

            var itemToDelete = await _repositoryItem.GetAll().FirstOrDefaultAsync(e => e.Id == input.Id);
            // Check if the item exists
            if(itemToDelete != null)
            {
                await _repositoryItem.DeleteAsync(itemToDelete.Id);

                var itemCategoriesToDelete = await _repositoryItemCategory.GetAllListAsync(e => e.ItemId == itemToDelete.Id);
                foreach (var item in itemCategoriesToDelete)
                {
                    await _repositoryItemCategory.DeleteAsync(item);
                }
            }
            else
            {
                throw new UserFriendlyException(00, L("ThisItemNotFound"));
            }


        }
    }
}
