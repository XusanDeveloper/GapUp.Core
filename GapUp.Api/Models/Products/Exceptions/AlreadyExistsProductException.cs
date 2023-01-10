using System;
using Xeptions;

namespace GapUp.Api.Models.Products.Exceptions
{
    public class AlreadyExistsProductException : Xeption
    {
        public AlreadyExistsProductException(Exception innerException)
            : base(message: "Product already exists.", innerException)
        { }
    }
}
