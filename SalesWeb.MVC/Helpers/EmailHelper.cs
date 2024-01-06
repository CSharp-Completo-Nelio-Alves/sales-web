using System.Text.RegularExpressions;

namespace SalesWeb.MVC.Helpers
{
    public static class EmailHelper
    {
        public const string RegexPattern = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";

        public static bool ValidateEmail(string email) => new Regex(RegexPattern, RegexOptions.IgnoreCase).IsMatch(email);
    }
}
