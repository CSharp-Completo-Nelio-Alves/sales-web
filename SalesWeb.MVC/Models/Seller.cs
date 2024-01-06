using Microsoft.AspNetCore.Mvc;
using SalesWeb.MVC.Helpers;
using SalesWeb.MVC.Helpers.ExtesionsMethods;
using SalesWeb.MVC.Models.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace SalesWeb.MVC.Models
{
    public class Seller : IComparable<Seller>
    {
        private const int _minimumLengthName = 2;
        private const double _minimumSalary = 1500.00;

        private string _name;
        private string _email;
        private DateTime _birthDate;
        private decimal _baseSalary;
        private Department _department;
        private List<SalesRecord> _sales = new();

        public const int MinimumAge = 16;

        public int Id { get; set; }
        public int DepartmentId { get; set; }

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

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessagesHelper.EmptyField)]
        [MinLength(_minimumLengthName, ErrorMessage = ErrorMessagesHelper.MinimumCharacter)]
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new DomainException(string.Format(ErrorMessagesHelper.EmptyField, nameof(Name)));

                var name = value.Trim();

                if (name.Length < _minimumLengthName)
                    throw new DomainException(string.Format(ErrorMessagesHelper.MinimumCharacter, nameof(Name), _minimumLengthName));

                _name = name;
            }
        }

        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessagesHelper.EmptyField)]
        [RegularExpression(EmailHelper.RegexPattern, ErrorMessage = ErrorMessagesHelper.invalidField)]
        public string Email
        {
            get => _email;
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new DomainException(string.Format(ErrorMessagesHelper.EmptyField, nameof(Email)));
                
                var email = value.Trim().ToLower();

                if (!EmailHelper.ValidateEmail(email))
                    throw new DomainException(string.Format(ErrorMessagesHelper.invalidField, nameof(Email)));

                _email = email;
            }
        }

        [Display(Name = "Birth Date")]
        [Required(ErrorMessage = ErrorMessagesHelper.EmptyField)]
        [DataType(DataType.Date, ErrorMessage = ErrorMessagesHelper.invalidField)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                if (value >= DateTime.Now)
                    throw new DomainException(string.Format(ErrorMessagesHelper.invalidField, "Birth Date"));

                var valid = value.ValidateAge(MinimumAge);

                if (!valid)
                    throw new DomainException(ErrorMessagesHelper.InvalidSellerAge);

                _birthDate = value;
            }
        }

        [Display(Name = "Base Salary")]
        [Required(ErrorMessage = ErrorMessagesHelper.EmptyField)]
        [DataType(DataType.Currency, ErrorMessage = ErrorMessagesHelper.invalidField)]
        [Range(_minimumSalary, double.MaxValue, ErrorMessage = ErrorMessagesHelper.MinimumSalary)]
        public decimal BaseSalary
        {
            get => _baseSalary;
            set
            {
                if (value <= 0)
                    throw new DomainException(string.Format(ErrorMessagesHelper.MinimumSalary, "Base Salary", _minimumSalary));

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

            return Email.CompareTo(other.Email);
        }

        #endregion
    }
}
