using System;
using Xeptions;

namespace GapUp.Api.Models.Products.Exceptions
{
    public class ProductServiceException : Xeption
    {
        public ProductServiceException(Exception innerException)
            : base(message: "Product service error occurred, contact support.", innerException)
        { }
    }
}
