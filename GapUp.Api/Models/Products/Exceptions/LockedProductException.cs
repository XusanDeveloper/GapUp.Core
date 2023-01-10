using System;
using Xeptions;

namespace GapUp.Api.Models.Products.Exceptions
{
    public class LockedProductException : Xeption
    {
        public LockedProductException(Exception innerException)
            : base(message: "Product is locked, please try again.", innerException)
        { }
    }
}
