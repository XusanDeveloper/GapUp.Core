using System;
using Xeptions;

namespace GapUp.Api.Models.Products.Exceptions
{
    public class ProductValidationException : Xeption
    {
        public ProductValidationException(Xeption innerException)
             : base(message: "Product validation error occured, fix the errors and try again.", innerException)
        { }
    }
}
