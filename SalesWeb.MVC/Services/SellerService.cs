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

        public void Create(Seller seller)
        {
            if (SellerExists(seller))
                return;

            _context.Add(seller);
            _context.SaveChanges();
        }

        public IEnumerable<Seller> GetAll() => _context.Seller.AsEnumerable();

        public bool SellerExists(Seller seller) => _context.Seller.FirstOrDefault(s => s.Email.Equals(seller.Email)) is not null;
    }
}
