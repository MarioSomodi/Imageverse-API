using ErrorOr;

namespace Imageverse.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Authentication
        {
            public static Error InvalidCredentials => Error.("User.InvalidCredentials", "Credentials you have entered are invalid.");
        }
    }
}
