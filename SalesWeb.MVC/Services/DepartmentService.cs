using SalesWeb.MVC.Data;
using SalesWeb.MVC.Models;

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
                return;

            _context.Department.Add(department);
            _context.SaveChanges();
        }

        public void Update(Department department)
        {
            _context.Department.Update(department);
            _context.SaveChanges();
        }

        public void Delete(Department department)
        {
            _context.Department.Remove(department);
            _context.SaveChanges();
        }

        public Department Get(int id) => _context.Department.Find(id);

        public IEnumerable<Department> FindAll() => _context.Department.OrderBy(d => d.Name).AsEnumerable();

        public bool DepartmentExists(Department department) => _context.Department.Any(d => d.Name.Equals(department.Name));
    }
}
