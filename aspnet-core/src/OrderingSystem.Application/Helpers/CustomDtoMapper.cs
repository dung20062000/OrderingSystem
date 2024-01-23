using AutoMapper;
using OrderingSystem.OrderSystem.Category;
using OrderingSystem.OrderSystem.Category.Dto;
using OrderingSystem.OrderSystem.Discount;
using OrderingSystem.OrderSystem.Item;
using OrderingSystem.OrderSystem.Item.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Helpers
{
    public class CustomDtoMapper : Profile
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<OdsItem, CreateOrEditItemDto>().ReverseMap();
            configuration.CreateMap<OdsCategory, CreateOrEditCategoryDto>().ReverseMap();
            configuration.CreateMap<OdsItemCategory, CreateOrEditItemDto>().ReverseMap();
            //configuration.CreateMap<OdsDiscount, CreateOrEditItemDto>().ReverseMap();
        }
    }
}
