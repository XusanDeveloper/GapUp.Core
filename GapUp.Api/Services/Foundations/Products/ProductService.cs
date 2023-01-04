using GapUp.Api.Brokers.DateTimes;
using GapUp.Api.Brokers.Loggings;
using GapUp.Api.Brokers.Storages;
using GapUp.Api.Models.Products;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GapUp.Api.Services.Foundations.Products
{
    public partial class ProductService : IProductService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;
        private readonly IDateTimeBroker dateTimeBroker;

        public ProductService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker,
            IDateTimeBroker dateTimeBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
            this.dateTimeBroker = dateTimeBroker;
        }

        public ValueTask<Product> AddProductAsync(Product product) =>
        TryCatch(async () =>
        {
            ValidateProductOnAdd(product);

            return await this.storageBroker.InsertProductAsync(product);
        });
        public IQueryable<Product> RetrieveAllProducts() =>
            TryCatch(() => this.storageBroker.SelectAllProducts());

        public ValueTask<Product> RetrieveProductByIdAsync(Guid productId) =>
        TryCatch(async () =>
        {
            ValidateProductId(productId);
            Product maybeProduct = await this.storageBroker.SelectProductByIdAsync(productId);
            ValidateStorageProductExists(maybeProduct, productId);

            return maybeProduct;
        });

        public ValueTask<Product> ModifyProductAsync(Product product) =>
        TryCatch(async () =>
        {
            ValidateProductOnModify(product);

            Product maybeProduct = await this.storageBroker.UpdateProductAsync(product);
            ValidateAginstStorageProductOnModify(inputProduct: product, storageProduct: maybeProduct);

            return await this.storageBroker.UpdateProductAsync(product);
        });

        public ValueTask<Product> RemoveProductByIdAsync(Guid productId) =>
        TryCatch(async () =>
        {
            ValidateProductId(productId);
            Product maybeProduct = await this.storageBroker.SelectProductByIdAsync(productId);
            ValidateStorageProduct(maybeProduct, productId);

            return await this.storageBroker.DeleteProductAsync(maybeProduct);
        });
    }
}
