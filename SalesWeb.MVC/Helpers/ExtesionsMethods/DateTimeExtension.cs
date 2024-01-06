namespace SalesWeb.MVC.Helpers.ExtesionsMethods
{
    public static class DateTimeExtension
    {
        public static int CalculateAge(this DateTime birthDate)
        {
            var currentDate = DateTime.Now;
            var age = currentDate.Year - birthDate.Year;

            if (currentDate.DayOfYear < birthDate.DayOfYear)
                age -= 1;

            return age;
        }

        public static bool ValidateAge(this DateTime birthDate, int minimumAge) => birthDate.CalculateAge() >= minimumAge;
    }
}
