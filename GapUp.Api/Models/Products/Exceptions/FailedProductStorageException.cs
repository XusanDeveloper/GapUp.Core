using System;
using Xeptions;

namespace GapUp.Api.Models.Products.Exceptions
{
    public class FailedProductStorageException : Xeption
    {
        public FailedProductStorageException(Exception innerException)
            : base(message: "Failed product storage error occurred, contact support.", innerException)
        { }
    }
}
