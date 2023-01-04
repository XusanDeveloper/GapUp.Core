using GapUp.Api.Models.Products;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GapUp.Api.Services.Foundations.Products
{
    public interface IProductService
    {
        ValueTask<Product> AddProductAsync(Product product);
        IQueryable<Product> RetrieveAllProducts();
        ValueTask<Product> RetrieveProductByIdAsync(Guid productId);
        ValueTask<Product> ModifyProductAsync(Product product);
        ValueTask<Product> RemoveProductByIdAsync(Guid productId);
    }
}
