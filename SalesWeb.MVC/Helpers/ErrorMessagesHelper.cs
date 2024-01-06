namespace SalesWeb.MVC.Helpers
{
    public static class ErrorMessagesHelper
    {
        public const string EmptyField = "{0} cannot be empty";
        public const string invalidField = "Invalid {0}";

        public const string MinimumCharacter = "{0} must be have {1} minimum character";
        public const string MinimumSalary = "{0} must be greater or equal than {1}";

        public const string InvalidSellerAge = "Age must be greater or equal than 16";
    }
}
