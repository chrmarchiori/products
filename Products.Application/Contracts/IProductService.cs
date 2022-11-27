using Products.Application.Dtos;
using Products.Persistence.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Products.Application.Contracts
{
    public interface IProductService
    {
        Task<ProductDto> GetProductByIdAsync(int productId);
        Task<PageList<ProductDto>> GetAllProductsAsync(PageParams pageParams);

        Task<ProductDto> AddProduct(ProductDto model);
        Task<ProductDto> UpdateProduct(int productId, ProductDto model);
        Task<bool> DeleteProduct(int productId);
    }
}
