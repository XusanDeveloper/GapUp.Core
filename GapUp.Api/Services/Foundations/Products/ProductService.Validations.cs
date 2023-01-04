using GapUp.Api.Models.Products;
using GapUp.Api.Models.Products.Exceptions;
using System;
using System.Net.Sockets;

namespace GapUp.Api.Services.Foundations.Products
{
    public partial class ProductService
    {
        private void ValidateProductOnAdd(Product product)
        {
            ValidateProductNotNull(product);

            Validate(
                (Rule: IsInvalid(product.Id), Parameter: nameof(Product.Id)),
                (Rule: IsInvalid(product.Title), Parameter: nameof(Product.Title)),
                (Rule: IsInvalid(product.Price), Parameter: nameof(Product.Price)),
                (Rule: IsInvalid(product.Description), Parameter: nameof(Product.Description)),
                (Rule: IsInvalid(product.CreatedDate), Parameter: nameof(Product.CreatedDate)),
                (Rule: IsInvalid(product.UpdatedDate), Parameter: nameof(Product.UpdatedDate)),
                (Rule: IsInvalid(product.CreatedUserId), Parameter: nameof(Product.CreatedUserId)),
                (Rule: IsInvalid(product.UpdatedUserId), Parameter: nameof(Product.UpdatedUserId)),
                (Rule: IsNotRecent(product.CreatedDate), Parameter: nameof(Product.CreatedDate)),

                (Rule: IsNotSame(
                    firstDate: product.CreatedDate,
                    secondDate: product.UpdatedDate,
                    secondDateName: nameof(Product.UpdatedDate)),
                Parameter: nameof(Product.CreatedDate)));
        }

        private void ValidateProductOnModify(Product product)
        {
            ValidateProductNotNull(product);
            Validate(
                (Rule: IsInvalid(product.Id), Parameter: nameof(Product.Id)),
                (Rule: IsInvalid(product.Title), Parameter: nameof(Product.Title)),
                (Rule: IsInvalid(product.Description), Parameter: nameof(Product.Description)),
                (Rule: IsInvalid(product.CreatedDate), Parameter: nameof(Product.CreatedDate)),
                (Rule: IsInvalid(product.UpdatedDate), Parameter: nameof(Product.UpdatedDate)),
                (Rule: IsInvalid(product.CreatedUserId), Parameter: nameof(Product.CreatedUserId)),
                (Rule: IsInvalid(product.UpdatedUserId), Parameter: nameof(Product.UpdatedUserId)),
                (Rule: IsNotRecent(product.UpdatedDate), Parameter: nameof(Product.UpdatedDate)),

                (Rule: IsSame(
                        firstDate: product.UpdatedDate,
                        secondDate: product.CreatedDate,
                        secondDateName: nameof(product.CreatedDate)),

                     Parameter: nameof(product.UpdatedDate)));
        }

        private void ValidateAginstStorageProductOnModify(Product inputProduct, Product storageProduct)
        {
            ValidateStorageProduct(storageProduct, inputProduct.Id);

            Validate(
                (Rule: IsNotSame(
                    firstDate: inputProduct.CreatedDate,
                    secondDate: storageProduct.CreatedDate,
                    secondDateName: nameof(Product.CreatedDate)),
                Parameter: nameof(Product.CreatedDate)),

                (Rule: IsSame(
                    firstDate: inputProduct.UpdatedDate,
                    secondDate: storageProduct.UpdatedDate,
                    secondDateName: nameof(Product.UpdatedDate)),
                Parameter: nameof(Product.UpdatedDate)));
        }

        private void ValidateProductId(Guid productId) =>
            Validate((Rule: IsInvalid(productId), Parameter: nameof(Product.Id)));

        private static dynamic IsNotSame
            (DateTimeOffset firstDate,
            DateTimeOffset secondDate,
            string secondDateName) => new
            {
                Condition = firstDate != secondDate,
                Message = $"Date is not same as {secondDateName}."
            };

        private dynamic IsSame(
            DateTimeOffset firstDate,
            DateTimeOffset secondDate,
            string secondDateName) => new
            {
                Condition = firstDate == secondDate,
                Message = $"Date is the same as {secondDateName}"
            };

        private dynamic IsNotRecent(DateTimeOffset date) => new
        {
            Condition = IsDateNotRecent(date),
            Message = "Date is not recent."
        };

        private bool IsDateNotRecent(DateTimeOffset date)
        {
            DateTimeOffset currentDateTime = this.dateTimeBroker.GetCurrentDateTime();
            TimeSpan timeDifference = currentDateTime.Subtract(date);

            return timeDifference.TotalSeconds is > 60 or < 0;
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == default,
            Message = "Id is required"
        };

        private static dynamic IsInvalid(decimal price) => new
        {
            Condition = price == default,
            Message = "Price is required"
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Value is required"
        };

        private void ValidateStorageProduct(Product maybeProduct, Guid productId)
        {
            if (maybeProduct is null)
            {
                throw new NotFoundProductException(productId);
            }
        }

        private static void ValidateProductNotNull(Product product)
        {
            if (product is null)
            {
                throw new NullProductException();
            }
        }

        private static void ValidateStorageProductExists(Product maybeProduct, Guid productId)
        {
            if (maybeProduct is null)
            {
                throw new NotFoundProductException(productId);
            }
        }

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidProductException = new InvalidProductException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidProductException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidProductException.ThrowIfContainsErrors();
        }
    }
}
