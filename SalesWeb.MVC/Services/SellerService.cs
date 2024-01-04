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
    }
}
