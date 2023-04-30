using System.Text.RegularExpressions;

namespace Imageverse.Domain.Common.Utils
{
    public static class RegexExpressions
    {
        public static Regex lowercase = new Regex("[a-z]+");
        public static Regex uppercase = new Regex("[A-Z]+");
        public static Regex digit = new Regex("(\\d)+");
        public static Regex symbol = new Regex("(\\W)+");
    }
}
