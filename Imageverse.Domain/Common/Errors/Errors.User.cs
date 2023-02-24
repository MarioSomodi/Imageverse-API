using ErrorOr;

namespace Imageverse.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class User
        {
            public static Error DuplicateEmail => Error.Conflict("User.DuplicateEmail", "User with given email alredy exists.");
        }
    }
}
