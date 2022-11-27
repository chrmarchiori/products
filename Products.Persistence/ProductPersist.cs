using Microsoft.EntityFrameworkCore;
using Products.Domain;
using Products.Persistence.Contexts;
using Products.Persistence.Contracts;
using Products.Persistence.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Persistence
{
    public class ProductPersist : IProductPersist
    {
        private readonly ProductsContext _context;

        public ProductPersist(ProductsContext context)
        {
            _context = context;
        }

        public async Task<PageList<Product>> GetAllProductsAsync(PageParams pageParams)
        {
            IQueryable<Product> products = _context.Products
                    .Where(p => p.Ativo && 
                        (p.Descricao.ToLower().Contains(pageParams.Term.ToLower()) ||
                        p.Descricao_Fornecedor.ToLower().Contains(pageParams.Term.ToLower()))
                    )
                    .OrderBy(p => p.Id);

            return await PageList<Product>.CreateAsync(products, pageParams.PageNumber, pageParams.pageSize);
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            Product product = await _context.Products.Where(p => p.Ativo && p.Id == productId).FirstOrDefaultAsync();
            return product;
        }

    }
}
