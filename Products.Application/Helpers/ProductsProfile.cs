using AutoMapper;
using Products.Application.Dtos;
using Products.Domain;
using Products.Persistence.Pagination;

namespace Products.Application.Helpers
{
    public class ProductsProfile : Profile
    {
        public ProductsProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
