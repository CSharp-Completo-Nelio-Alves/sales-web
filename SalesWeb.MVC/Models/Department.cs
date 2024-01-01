﻿namespace SalesWeb.MVC.Models
{
    public class Department : IComparable<Department>
    {
        private string _name;

        public int Id { get; set; }

        public Department(string name)
        {
            Name = name;
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

        public override string ToString() => $"Id: {Id}, Name: {Name}";

        #region Compare Methods

        public override int GetHashCode()
        {
            var baseNumber = 3;

            return Name.GetHashCode() * baseNumber;
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (obj is not Department)
                return false;

            var other = obj as Department;

            return Name.Equals(other.Name);
        }

        public int CompareTo(Department department)
        {
            if (department is null)
                return 1;

            if (ReferenceEquals(this, department))
                return 0;

            return Name.CompareTo(department.Name);
        }

        #endregion
    }
}