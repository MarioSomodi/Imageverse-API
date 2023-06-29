using ErrorOr;

namespace Imageverse.Domain.Common.AppErrors
{
    public static partial class Errors
    {
        public static class User
        {
            public static Error DuplicateEmail => Error.Conflict("User.DuplicateEmail", "User with given email alredy exists.");
            public static Error PackageConflict => Error.Conflict("User.PackageConflict", "You are already have that package assigned.");
        }
    }
}
