using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
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
        public async Task CreateOrEdit(CreateOrEditItemDto input)
        {
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                //await Update(input);
            }
        }
        protected virtual async Task Create(CreateOrEditItemDto input)
        {
            //var tenantId = AbpSession.TenantId;
            //var itemCount = await _repositoryItem.GetAll().Where(e => e.TenantId == tenantId).Count();
            var itemMapper = ObjectMapper.Map<OdsItem>(input);
            await _repositoryItem.InsertAsync(itemMapper);
        }
    }
}
