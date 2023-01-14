using System;
using Xeptions;

namespace GapUp.Api.Models.Users.Exceptions
{
    public class AlreadyExistsUserException : Xeption
    {
        public AlreadyExistsUserException(Exception innerException)
            : base(message: "Failed user dependency validation error occurred, fix errors and try again.", innerException)
        { }
    }
}
