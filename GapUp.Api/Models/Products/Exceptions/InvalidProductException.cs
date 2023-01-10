using Xeptions;

namespace GapUp.Api.Models.Products.Exceptions
{
    public class InvalidProductException : Xeption
    {
        public InvalidProductException()
            : base(message: "Product is invalid.")
        { }
    }
}
