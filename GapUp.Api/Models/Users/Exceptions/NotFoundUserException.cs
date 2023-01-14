using System;
using Xeptions;

namespace GapUp.Api.Services.Foundations.Users
{
    public class NotFoundUserException : Xeption
    {
        public NotFoundUserException(Guid userId)
            : base(message: $"Could not find user with id:{userId}.")
        { }
    }
}