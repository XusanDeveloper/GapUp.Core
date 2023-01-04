using System;
using Xeptions;

namespace GapUp.Api.Models.Products.Exceptions
{
    public class FailedProductServiceException : Xeption
    {
        public FailedProductServiceException(Exception innerException)
            : base(message: "Failed product service occurred, please contact support.", innerException)
        { }
    }
}
