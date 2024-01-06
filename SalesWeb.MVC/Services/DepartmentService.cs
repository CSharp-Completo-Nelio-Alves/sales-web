using Microsoft.EntityFrameworkCore;
using SalesWeb.MVC.Data;
using SalesWeb.MVC.Helpers;
using SalesWeb.MVC.Models;
using SalesWeb.MVC.Models.Exceptions;
using SalesWeb.MVC.Services.Exceptions;

namespace SalesWeb.MVC.Services
{
    public class DepartmentService
    {
        private readonly SalesWebContext _context;

        public DepartmentService(SalesWebContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Department department)
        {
            if (await DepartmentExistsAsync(department))
                throw new DomainException("Department already registered");

            _context.Department.Add(department);
            
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Department department)
        {
            if (!await DepartmentExistsAsync(department.Id))
                throw new NotFoundException("Id not found");

            try
            {
                _context.Department.Update(department);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbConcurrencyException(ex.Message);
            }
        }

        public async Task DeleteAsync(Department department)
        {
            try
            {
                _context.Department.Remove(department);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new IntegrityException(ErrorMessagesHelper.InvalidDeleteDepartment);
            }
        }

        public async Task<Department> GetAsync(int id) => await _context.Department.FindAsync(id);

        public async Task<IEnumerable<Department>> GetAllAsync() => await _context.Department.OrderBy(d => d.Name).ToListAsync();

        public async Task<bool> DepartmentExistsAsync(int id) => await _context.Department.AnyAsync(d => d.Id == id);

        public async Task<bool> DepartmentExistsAsync(Department department) => await _context.Department.AnyAsync(d => d.Name.Equals(department.Name));
    }
}
