using AutoMapper;
using Products.Application.Contracts;
using Products.Application.Dtos;
using Products.Domain;
using Products.Persistence.Contracts;
using Products.Persistence.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Application
{
    public class ProductService : IProductService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IProductPersist _productPersist;

        private readonly IMapper _mapper;

        public ProductService(IGeralPersist geralPersist, 
                              IProductPersist productPersist, 
                              IMapper mapper)
        {
            _geralPersist = geralPersist;
            _productPersist = productPersist;
            _mapper = mapper;
        }



        public async Task<ProductDto> AddProduct(ProductDto model)
        {
            try
            {
                if (model.Data_Fabricacao >= model.Data_Validade)
                    throw new Exception("Não é possível realizar essa operação pois a data de fabricação é maior ou igual a data de validade");

                var product = _mapper.Map<Product>(model);
                product.Ativo = true;
                _geralPersist.Add(product);

                if (await _geralPersist.SaveChangesAsync())
                {
                    var productReturn = await _productPersist.GetProductByIdAsync(product.Id);
                    return _mapper.Map<ProductDto>(productReturn);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ProductDto> UpdateProduct(int productId, ProductDto model)
        {
            try
            {
                if (model.Data_Fabricacao >= model.Data_Validade)
                    throw new Exception("Não é possível realizar essa operação pois a data de fabricação é maior ou igual a data de validade");

                var product = await _productPersist.GetProductByIdAsync(productId);
                if (product == null) return null;

                model.Id = product.Id;
                _mapper.Map(model, product);

                _geralPersist.Update(product);

                if (await _geralPersist.SaveChangesAsync())
                {
                    var productReturn = await _productPersist.GetProductByIdAsync(product.Id);
                    return _mapper.Map<ProductDto>(product);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            try
            {
                var product = await _productPersist.GetProductByIdAsync(productId);
                if (product == null) throw new Exception("Produto não encontrado");

                product.Ativo = false;

                _geralPersist.Update(product);
                return await _geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<ProductDto>> GetAllProductsAsync(PageParams pageParams)
        {
            try
            {
                var products = await _productPersist.GetAllProductsAsync(pageParams);
                if (products == null) return null;

                var result = _mapper.Map<PageList<ProductDto>>(products);

                result.CurrentPage = products.CurrentPage;
                result.TotalPages = products.TotalPages;
                result.PageSize = products.PageSize;
                result.TotalCount = products.TotalCount;

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ProductDto> GetProductByIdAsync(int productId)
        {
            try
            {
                var product = await _productPersist.GetProductByIdAsync(productId);
                if (product == null) return null;

                var result = _mapper.Map<ProductDto>(product);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
    }
}
