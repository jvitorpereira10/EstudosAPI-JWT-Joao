using AutoMapper;
using Ok.Tech.Api.Models.Price;
using Ok.Tech.Api.Models.PriceList;
using Ok.Tech.Api.Models.Product;
using Ok.Tech.Domain.Entities;

namespace Ok.Tech.Api.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Product, ProductModel>().ReverseMap();

            CreateMap<PriceList, PriceListModel>().ReverseMap();

            CreateMap<Price, PriceModel>().ReverseMap();

            CreateMap<Price, PriceViewModel>()
                .ForMember(p => p.PriceListName, o => o.MapFrom(p => p.PriceList.Name))
                .ForMember(p => p.ProductName, o => o.MapFrom(p => p.Product.Name));
        }
    }
}