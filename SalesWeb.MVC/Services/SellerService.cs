using Microsoft.EntityFrameworkCore;
using SalesWeb.MVC.Data;
using SalesWeb.MVC.Helpers;
using SalesWeb.MVC.Models;
using SalesWeb.MVC.Models.Exceptions;
using SalesWeb.MVC.Services.Exceptions;

namespace SalesWeb.MVC.Services
{
    public class SellerService
    {
        private readonly SalesWebContext _context;

        public SellerService(SalesWebContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Seller seller)
        {
            if (await SellerExistsAsync(seller))
                throw new DomainException(string.Format(ErrorMessagesHelper.EntityAlreadyRegistered, nameof(Seller)));

            _context.Add(seller);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Seller seller)
        {
            if (!await SellerExistsAsync(seller.Id))
                throw new NotFoundException(string.Format(ErrorMessagesHelper.EntityNotFound, nameof(Seller)));

            try
            {
                _context.Update(seller);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbConcurrencyException(ex.Message);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var seller = await GetAsync(id);

            if (seller is null)
                return;

            try
            {
                _context.Seller.Remove(seller);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new IntegrityException(ErrorMessagesHelper.InvalidDeleteSeller);
            }
        }

        public async Task<Seller> GetAsync(int id) => await _context.Seller.Include(d => d.Department).FirstOrDefaultAsync(d => d.Id == id);

        public async Task<IEnumerable<Seller>> GetAllAsync() => await _context.Seller.ToListAsync();

        public async Task<bool> SellerExistsAsync(int id) => await _context.Seller.AnyAsync(d => d.Id == id);

        public async Task<bool> SellerExistsAsync(Seller seller) => await _context.Seller.FirstOrDefaultAsync(s => s.Email.Equals(seller.Email)) is not null;
    }
}
