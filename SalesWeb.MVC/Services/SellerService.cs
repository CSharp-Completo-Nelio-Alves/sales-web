using SalesWeb.MVC.Data;
using SalesWeb.MVC.Models;

namespace SalesWeb.MVC.Services
{
    public class SellerService
    {
        private readonly SalesWebContext _context;

        public SellerService(SalesWebContext context)
        {
            _context = context;
        }

        public IEnumerable<Seller> FindAll() => _context.Seller.AsEnumerable();

        public void Add(Seller seller)
        {
            if (ValidateIfAlredyRegistered(seller))
                return;

            _context.Add(seller);
            _context.SaveChanges();
        }

        public bool ValidateIfAlredyRegistered(Seller seller) => _context.Seller.FirstOrDefault(s => s.Email.Equals(seller.Email)) is not null;
    }
}
