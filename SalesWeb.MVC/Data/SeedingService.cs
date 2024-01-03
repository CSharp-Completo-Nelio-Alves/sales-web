using Microsoft.EntityFrameworkCore;
using SalesWeb.MVC.Models;
using SalesWeb.MVC.Models.Enums;

namespace SalesWeb.MVC.Data
{
    public class SeedingService
    {
        private readonly SalesWebContext _context;

        public SeedingService(SalesWebContext context)
        {
            _context = context;

            _context.Database.Migrate();
        }

        public void Seed()
        {
            if (DataBaseIsEmpty())
            {
                var departments = new List<Department>()
                {
                    new("Computers"),
                    new("Electronics"),
                    new("Fashion"),
                    new("Books"),
                };

                var selles = new List<Seller>()
                {
                    new("Bob Brown", "bob@gmail.com", new DateTime(1998, 4, 21), 1000.0M, departments[0]),
                    new("Maria Green", "maria@gmail.com", new DateTime(1979, 12, 31), 3500.0M, departments[1]),
                    new("Alex Grey", "alex@gmail.com", new DateTime(1988, 1, 15), 2200.0M, departments[0]),
                    new("Martha Red", "martha@gmail.com", new DateTime(1993, 11, 30), 3000.0M, departments[3]),
                    new("Donald Blue", "donald@gmail.com", new DateTime(2000, 1, 9), 4000.0M, departments[2]),
                    new("Alex Pink", "bob@gmail.com", new DateTime(1997, 3, 4), 3000.0M, departments[1]),
                };

                var salesRecord = new List<SalesRecord>()
                {
                    new(new DateTime(2018, 09, 25), SaleStatus.Billed, 11000.0M, selles[0]),
                    new(new DateTime(2018, 09, 4), SaleStatus.Billed, 7000.0M, selles[4]),
                    new(new DateTime(2018, 09, 13), SaleStatus.Canceled, 4000.0M, selles[3]),
                    new(new DateTime(2018, 09, 1), SaleStatus.Billed, 8000.0M, selles[0]),
                    new(new DateTime(2018, 09, 21), SaleStatus.Billed, 3000.0M, selles[2]),
                    new(new DateTime(2018, 09, 15), SaleStatus.Billed, 2000.0M, selles[0]),
                    new(new DateTime(2018, 09, 28), SaleStatus.Billed, 13000.0M, selles[1]),
                    new(new DateTime(2018, 09, 11), SaleStatus.Billed, 4000.0M, selles[3]),
                    new(new DateTime(2018, 09, 14), SaleStatus.Pending, 11000.0M, selles[5]),
                    new(new DateTime(2018, 09, 7), SaleStatus.Billed, 9000.0M, selles[5]),
                    new(new DateTime(2018, 09, 13), SaleStatus.Billed, 6000.0M, selles[1]),
                    new(new DateTime(2018, 09, 25), SaleStatus.Pending, 7000.0M, selles[2]),
                    new(new DateTime(2018, 09, 29), SaleStatus.Billed, 10000.0M, selles[3]),
                    new(new DateTime(2018, 09, 4), SaleStatus.Billed, 3000.0M, selles[4]),
                    new(new DateTime(2018, 09, 12), SaleStatus.Billed, 4000.0M, selles[0]),
                    new(new DateTime(2018, 10, 5), SaleStatus.Billed, 2000.0M, selles[3]),
                    new(new DateTime(2018, 10, 1), SaleStatus.Billed, 12000.0M, selles[0]),
                    new(new DateTime(2018, 10, 24), SaleStatus.Billed, 6000.0M, selles[2]),
                    new(new DateTime(2018, 10, 22), SaleStatus.Billed, 8000.0M, selles[4]),
                    new(new DateTime(2018, 10, 15), SaleStatus.Billed, 8000.0M, selles[5]),
                    new(new DateTime(2018, 10, 17), SaleStatus.Billed, 9000.0M, selles[1]),
                    new(new DateTime(2018, 10, 24), SaleStatus.Billed, 4000.0M, selles[3]),
                    new(new DateTime(2018, 10, 19), SaleStatus.Canceled, 11000.0M, selles[1]),
                    new(new DateTime(2018, 10, 12), SaleStatus.Billed, 8000.0M, selles[4]),
                    new(new DateTime(2018, 10, 31), SaleStatus.Billed, 7000.0M, selles[2]),
                    new(new DateTime(2018, 10, 6), SaleStatus.Billed, 5000.0M, selles[3]),
                    new(new DateTime(2018, 10, 13), SaleStatus.Pending, 9000.0M, selles[0]),
                    new(new DateTime(2018, 10, 7), SaleStatus.Billed, 4000.0M, selles[2]),
                    new(new DateTime(2018, 10, 23), SaleStatus.Billed, 12000.0M, selles[4]),
                    new(new DateTime(2018, 10, 12), SaleStatus.Billed, 5000.0M, selles[1]),
                };

                _context.Department.AddRange(departments);
                _context.Seller.AddRange(selles);
                _context.SalesRecord.AddRange(salesRecord);

                _context.SaveChanges();
            }
        }

        private bool DataBaseIsEmpty() => !_context.Department.Any() && !_context.Seller.Any() && !_context.SalesRecord.Any();
    }
}
