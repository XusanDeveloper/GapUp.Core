using EFxceptions.Models.Exceptions;
using GapUp.Api.Models.Products;
using GapUp.Api.Models.Products.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xeptions;

namespace GapUp.Api.Services.Foundations.Products
{
    public partial class ProductService
    {
        private delegate ValueTask<Product> ReturningProductFunction();
        private delegate IQueryable<Product> ReturningProductsFunction();

        private async ValueTask<Product> TryCatch(ReturningProductFunction returningProductFunction)
        {
            try
            {
                return await returningProductFunction();
            }
            catch (NullProductException nullProductException)
            {
                throw CreateAndLogValidationException(nullProductException);
            }
            catch (InvalidProductException invalidProductException)
            {
                throw CreateAndLogValidationException(invalidProductException);
            }
            catch (NotFoundProductException notFoundProductException)
            {
                throw CreateAndLogValidationException(notFoundProductException);
            }
            catch (SqlException sqlException)
            {
                var failedProductStorageException = new FailedProductStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(failedProductStorageException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsProductException = new AlreadyExistsProductException(duplicateKeyException);

                throw CreateAndDependencyValidationException(alreadyExistsProductException);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                var lockedProductException = new LockedProductException(dbUpdateConcurrencyException);

                throw CreateAndDependencyValidationException(lockedProductException);
            }
            catch (DbUpdateException databaseUpdateException)
            {
                var failedProductStorageException = new FailedProductStorageException(databaseUpdateException);

                throw CreateAndLogDependencyException(failedProductStorageException);
            }
            catch (Exception serviceException)
            {
                var failedProductServiceException = new FailedProductServiceException(serviceException);

                throw CreateAndLogServiceException(failedProductServiceException);
            }
        }

        private IQueryable<Product> TryCatch(ReturningProductsFunction returningProductsFunction)
        {
            try
            {
                return returningProductsFunction();
            }
            catch (SqlException sqlException)
            {
                var failedProductStorageException = new FailedProductStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(failedProductStorageException);
            }
            catch (Exception serviceException)
            {
                var failedProductServiceException = new FailedProductServiceException(serviceException);

                throw CreateAndLogServiceException(failedProductServiceException);
            }
        }

        private ProductServiceException CreateAndLogServiceException(Exception exception)
        {
            var productServiceException = new ProductServiceException(exception);
            this.loggingBroker.LogError(productServiceException);

            return productServiceException;
        }

        private ProductDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var productDependencyException = new ProductDependencyException(exception);
            this.loggingBroker.LogError(productDependencyException);

            return productDependencyException;
        }

        private ProductValidationException CreateAndLogValidationException(Xeption exception)
        {
            var productValidationExpcetion = new ProductValidationException(exception);
            this.loggingBroker.LogError(productValidationExpcetion);

            return productValidationExpcetion;
        }

        private ProductDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var productDependencyException = new ProductDependencyException(exception);
            this.loggingBroker.LogCritical(productDependencyException);

            return productDependencyException;
        }

        private ProductDependencyValidationException CreateAndDependencyValidationException(Xeption exception)
        {
            var productDependencyValidationException = new ProductDependencyValidationException(exception);
            this.loggingBroker.LogError(productDependencyValidationException);

            return productDependencyValidationException;
        }
    }
}
