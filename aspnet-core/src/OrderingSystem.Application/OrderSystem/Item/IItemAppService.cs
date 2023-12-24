using Abp.Application.Services;
using OrderingSystem.OrderSystem.Item.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.OrderSystem.Item
{
    public interface IItemAppService : IApplicationService
    {
        Task CreateOrEdit(CreateOrEditItemDto itemDto);
    }
}
