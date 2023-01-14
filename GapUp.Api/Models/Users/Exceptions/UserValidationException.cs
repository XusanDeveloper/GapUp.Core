﻿using Xeptions;

namespace GapUp.Api.Models.Users.Exceptions
{
    public class UserValidationException : Xeption
    {
        public UserValidationException(Xeption innerExeption)
            : base(message: "User validation error occured, fix the errors and try again.", innerExeption)
        { }
    }
}
