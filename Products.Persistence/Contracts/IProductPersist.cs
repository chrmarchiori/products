using Products.Domain;
using Products.Persistence.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Persistence.Contracts
{
    public interface IProductPersist
    {
        Task<Product> GetProductByIdAsync(int productId);
        Task<PageList<Product>> GetAllProductsAsync(PageParams pageParams);
    }
}
