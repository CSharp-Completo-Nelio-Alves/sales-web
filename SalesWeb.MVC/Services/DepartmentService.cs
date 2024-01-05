using Microsoft.EntityFrameworkCore;
using SalesWeb.MVC.Data;
using SalesWeb.MVC.Models;
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

        public void Create(Department department)
        {
            if (DepartmentExists(department))
                throw new ApplicationException("Department already registered");

            _context.Department.Add(department);
            _context.SaveChanges();
        }

        public void Update(Department department)
        {
            if (!DepartmentExists(department.Id))
                throw new NotFoundException("Id not found");

            try
            {
                _context.Department.Update(department);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbConcurrencyException(ex.Message);
            }
        }

        public void Delete(Department department)
        {
            _context.Department.Remove(department);
            _context.SaveChanges();
        }

        public Department Get(int id) => _context.Department.Find(id);

        public IEnumerable<Department> GetAll() => _context.Department.OrderBy(d => d.Name).AsEnumerable();

        public bool DepartmentExists(int id) => _context.Department.Any(d => d.Id == id);

        public bool DepartmentExists(Department department) => _context.Department.Any(d => d.Name.Equals(department.Name));
    }
}
