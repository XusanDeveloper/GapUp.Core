using GapUp.Api.Models.Products;
using GapUp.Api.Models.Products.Exceptions;
using GapUp.Api.Services.Foundations.Products;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GapUp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : RESTFulController
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService) =>
            this.productService = productService;

        [HttpPost]
        public async ValueTask<ActionResult<Product>> PostProductAsync(Product product)
        {
            try
            {
                return await this.productService.AddProductAsync(product);
            }
            catch (ProductValidationException productValidationException)
            {
                return BadRequest(productValidationException.InnerException);
            }
            catch (ProductDependencyValidationException productDependencyValidationException)
                when (productDependencyValidationException.InnerException is AlreadyExistsProductException)
            {
                return Conflict(productDependencyValidationException.InnerException);
            }
            catch (ProductDependencyValidationException productDependencyValidationException)
            {
                return BadRequest(productDependencyValidationException.InnerException);
            }
            catch (ProductDependencyException productDependencyException)
            {
                return InternalServerError(productDependencyException.InnerException);
            }
            catch (ProductServiceException productServiceException)
            {
                return InternalServerError(productServiceException.InnerException);
            }
        }

        [HttpGet]
        public ActionResult<IQueryable<Product>> GetAllProducts()
        {
            try
            {
                IQueryable<Product> allProducts = this.productService.RetrieveAllProducts();

                return Ok(allProducts);
            }
            catch (ProductDependencyException productDependencyException)
            {
                return InternalServerError(productDependencyException.InnerException);
            }
            catch (ProductServiceException productServiceException)
            {
                return InternalServerError(productServiceException.InnerException);
            }
        }

        [HttpGet("{productId}")]
        public async ValueTask<ActionResult<Product>> GetProductByIdAsync(Guid id)
        {
            try
            {
                return await this.productService.RetrieveProductByIdAsync(id);
            }
            catch (ProductDependencyException productDependencyException)
            {
                return InternalServerError(productDependencyException.InnerException);
            }
            catch (ProductValidationException productValidationException)
                when (productValidationException.InnerException is InvalidProductException)
            {
                return BadRequest(productValidationException.InnerException);
            }
            catch (ProductValidationException productValidationException)
                when (productValidationException.InnerException is InvalidProductException)
            {
                return NotFound(productValidationException.InnerException);
            }
            catch (ProductServiceException productServiceException)
            {
                return InternalServerError(productServiceException.InnerException);
            }
        }

        [HttpPut]
        public async ValueTask<ActionResult<Product>> PutProductAsync(Product product)
        {
            try
            {
                Product modifiedProduct = await this.productService.ModifyProductAsync(product);

                return Ok(modifiedProduct);
            }
            catch (ProductValidationException productValidationException)
                when (productValidationException.InnerException is NotFoundProductException)
            {
                return NotFound(productValidationException.InnerException);
            }
            catch (ProductValidationException productValidationException)
            {
                return BadRequest(productValidationException.InnerException);
            }
            catch (ProductDependencyValidationException productDependencyValidationException)
                when (productDependencyValidationException.InnerException is AlreadyExistsProductException)
            {
                return Conflict(productDependencyValidationException.InnerException);
            }
            catch (ProductDependencyException productDependencyException)
            {
                return InternalServerError(productDependencyException.InnerException);
            }
            catch (ProductServiceException productServiceException)
            {
                return InternalServerError(productServiceException.InnerException);
            }
        }

        [HttpDelete("{productId}")]
        public async ValueTask<ActionResult<Product>> DeleteProductByIdAsync(Guid productId)
        {
            try
            {
                Product deletedProduct = await this.productService.RemoveProductByIdAsync(productId);

                return Ok(deletedProduct);
            }
            catch (ProductValidationException productValidationException)
                when (productValidationException.InnerException is NotFoundProductException)
            {
                return NotFound(productValidationException.InnerException);
            }
            catch (ProductValidationException productValidationException)
            {
                return BadRequest(productValidationException.InnerException);
            }
            catch (ProductDependencyValidationException productDependencyValidationException)
                when (productDependencyValidationException.InnerException is LockedProductException)
            {
                return Locked(productDependencyValidationException.InnerException);
            }
            catch (ProductDependencyValidationException productDependencyValidationException)
            {
                return BadRequest(productDependencyValidationException.InnerException);
            }
            catch (ProductDependencyException productDependencyException)
            {
                return InternalServerError(productDependencyException.InnerException);
            }
            catch (ProductServiceException productServiceException)
            {
                return InternalServerError(productServiceException.InnerException);
            }
        }
    }
}
