﻿using Imageverse.Domain.Common.Enums;

namespace Imageverse.Application.Common.Interfaces.Services
{
    public interface IDatabaseLogger
    {
        public Task LogUserAction(UserActions userAction, string message);
    }
}
