using Abp.Application.Services;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using OrderingSystem.OrderSystem.Discount.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.OrderSystem.Discount
{
    public class DiscountAppService : ApplicationService, IDiscountAppService
    {
        private readonly IRepository<OdsDiscount, long> _repositoryDiscount;
        public DiscountAppService
        (
            IRepository<OdsDiscount, long> repositoryDiscount
        )
        {
            _repositoryDiscount = repositoryDiscount;
        }
        public async Task<List<GetDiscountForViewDto>> GetAllForCreateItem()
        {
            var query = from disc in _repositoryDiscount.GetAll().Where(e => e.IsDeleted == false)
                        select new GetDiscountForViewDto
                        {
                            Id = disc.Id,
                            DiscountName = disc.DiscountName,
                        };
            return await query.ToListAsync();
        }
    }
}
