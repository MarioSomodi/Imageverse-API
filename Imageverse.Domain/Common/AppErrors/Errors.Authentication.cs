using ErrorOr;

namespace Imageverse.Domain.Common.AppErrors
{
    public static partial class Errors
    {
        public static class Authentication
        {
            public static Error InvalidCredentials => Error.Validation("User.InvalidCredentials", "Credentials you have entered are invalid.");
            public static Error InvalidPassword => Error.Validation("User.InvalidPassword", "The current password you have entered is invalid.");
        }
    }
}
