using SalesWeb.MVC.Models.Enums;

namespace SalesWeb.MVC.Models
{
    public class SalesRecord : IComparable<SalesRecord>
    {
        private decimal _amount;
        private Seller _seller;

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public SaleStatus Status { get; set; }

        public SalesRecord()
        {
            
        }

        public SalesRecord(DateTime date, SaleStatus status, decimal amount)
        {
            Date = date;
            Status = status;
            Amount = amount;
        }

        public decimal Amount
        {
            get => _amount;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Amount must be greather than 0");

                _amount = value;
            }
        }

        public Seller Seller
        {
            get => _seller;
            set
            {
                if (value is null)
                    throw new ArgumentNullException(nameof(Seller));

                _seller = value;
            }
        }

        public override string ToString() => $"Sales Status {Status}; Date: {Date:yyyy/MM/dd HH:mm:ss}; Amount: {Amount:C2}";

        #region Compare Methods

        public override int GetHashCode()
        {
            var baseNumber = 5;

            return Date.GetHashCode() * baseNumber;
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (obj is not SalesRecord other)
                return false;

            return Date.Equals(other.Date);
        }

        public int CompareTo(SalesRecord other)
        {
            if (other is null)
                return 1;

            if (ReferenceEquals(this, other))
                return 0;

            return Date.CompareTo(other.Date);
        }

        #endregion
    }
}
