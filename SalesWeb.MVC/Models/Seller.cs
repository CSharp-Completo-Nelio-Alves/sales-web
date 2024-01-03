using System.Text.RegularExpressions;

namespace SalesWeb.MVC.Models
{
    public class Seller : IComparable<Seller>
    {
        private string _name;
        private string _email;
        private DateTime _birthDate;
        private decimal _baseSalary;
        private Department _department;
        private List<SalesRecord> _sales = new();

        public const int MinimumAge = 16;

        public int Id { get; set; }
        public ICollection<SalesRecord> Sales => _sales.AsReadOnly();

        public Seller()
        {
            
        }

        public Seller(string name, string email, DateTime birthDate, decimal baseSalary, Department department)
        {
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be empty");

                _name = value.Trim();
            }
        }

        public string Email
        {
            get => _email;
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("E-mail cannot be empty");
                
                var email = value.Trim().ToLower();

                if (!ValidateEmail(email))
                    throw new ArgumentException("Invalid e-mail");

                _email = email;
            }
        }

        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                if (value >= DateTime.Now)
                    throw new ArgumentException("Invalid birth date");

                var age = CalculateAge(value);

                if (age < MinimumAge)
                    throw new ArgumentException($"Age must be greather than {MinimumAge}");

                _birthDate = value;
            }
        }

        public decimal BaseSalary
        {
            get => _baseSalary;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Base salary must be greather than 0");

                _baseSalary = value;
            }
        }

        public Department Department
        {
            get => _department;
            set
            {
                if (value is null)
                    throw new ArgumentNullException(nameof(Department));

                _department = value;
            }
        }

        public void AddSales(SalesRecord sales) => _sales.Add(sales);

        public void RemoveSales(SalesRecord sales) => _sales.Remove(sales);

        public decimal TotalSales(DateTime initial, DateTime final) =>
            _sales.Where(s => s.Date.Date >= initial.Date && s.Date.Date <= final.Date).Select(s => s.Amount).DefaultIfEmpty().Sum();

        public override string ToString() => $"Name: {Name}; E-mail: {Email}; Salary: {BaseSalary:C2}";

        #region Compare Methods

        public override int GetHashCode()
        {
            var baseNumber = 7;

            return Email.GetHashCode() * baseNumber;
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (obj is not Seller other)
                return false;

            return Email.Equals(other.Email);
        }

        public int CompareTo(Seller other)
        {
            if (other is null)
                return 1;

            if (ReferenceEquals (this, other))
                return 0;

            return Name.CompareTo(other.Name);
        }

        #endregion

        #region Helpers Methods

        private bool ValidateEmail(string email) =>
            new Regex(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$", RegexOptions.IgnoreCase).IsMatch(email);

        private int CalculateAge(DateTime birthDate)
        {
            var currentDate = DateTime.Now;
            var age = currentDate.Year - birthDate.Year;

            if (currentDate.DayOfYear < birthDate.DayOfYear)
                age -= 1;

            return age;
        }

        #endregion
    }
}
