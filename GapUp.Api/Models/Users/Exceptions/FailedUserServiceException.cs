using System;
using Xeptions;

namespace GapUp.Api.Models.Users.Exceptions
{
    public class FailedUserServiceException : Xeption
    {
        public FailedUserServiceException(Exception innerException)
            : base(message: "Failed user service occured, please contact support", innerException)
        { }
    }
}
