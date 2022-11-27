using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Products.Application.Contracts;
using Products.Application.Dtos;
using Products.Persistence.Pagination;
using System;
using System.Threading.Tasks;

namespace Products.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] PageParams pageParams)
        {
            try
            {
                var products = await _productService.GetAllProductsAsync(pageParams);
                if (products == null) return NoContent();

                return Ok(products);
            }
            catch (Exception ex)
            {
                return this.StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar produtos. Erro: {ex.Message}"
                );
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null) return NoContent();

                return Ok(product);
            }
            catch (Exception ex)
            {
                return this.StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar produto {id}. Erro: {ex.Message}"
                );
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProductDto model)
        {
            try
            {
                if (model.Data_Fabricacao >= model.Data_Validade) 
                    return BadRequest("Não é possível realizar essa operação pois a data de fabricação é maior ou igual a data de validade");

                var product = await _productService.AddProduct(model);
                if (product == null) return BadRequest("Erro ao tentar adicionar produto informado");

                return Ok(product);
            }
            catch (Exception ex)
            {
                return this.StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar adicionar produto. Erro: {ex.Message}"
                );
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ProductDto model)
        {
            try
            {
                if (model.Data_Fabricacao >= model.Data_Validade)
                    return BadRequest("Não é possível realizar essa operação pois a data de fabricação é maior ou igual a data de validade");

                var product = await _productService.UpdateProduct(id, model);
                if (product == null) return NoContent();

                return Ok(product);
            }
            catch (Exception ex)
            {
                return this.StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar atualizar produto. Erro: {ex.Message}"
                );
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null) return NoContent();

                return await _productService.DeleteProduct(id)
                    ? Ok(new { message = "Produto deletado com sucesso." })
                    : throw new Exception($"Ocorreu um problema não especificado ao tentar deletar o produto {id}.");
            }
            catch (Exception ex)
            {
                return this.StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar excluir produto {id}. Erro: {ex.Message}"
                );
            }
        }
    }
}
