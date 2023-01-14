using Xeptions;

namespace GapUp.Api.Models.Users.Exceptions
{
    public class InvalidUserException : Xeption
    {
        public InvalidUserException()
          : base(message: "User is invalid.")
        { }
    }
}
