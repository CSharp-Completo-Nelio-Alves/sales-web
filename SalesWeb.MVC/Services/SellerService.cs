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

        public void Delete(int id)
        {
            var seller = Get(id);

            if (seller is null)
                return;

            _context.Seller.Remove(seller);
            _context.SaveChanges();
        }

        public Seller Get(int id) => _context.Seller.Find(id);

        public IEnumerable<Seller> GetAll() => _context.Seller.AsEnumerable();

        public bool SellerExists(Seller seller) => _context.Seller.FirstOrDefault(s => s.Email.Equals(seller.Email)) is not null;
    }
}
