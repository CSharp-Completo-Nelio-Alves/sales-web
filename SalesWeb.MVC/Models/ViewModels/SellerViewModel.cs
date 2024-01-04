namespace SalesWeb.MVC.Models.ViewModels
{
    public class SellerViewModel
    {
        public Seller Seller { get; set; }
        public IEnumerable<Department> Departments { get; set; } = Enumerable.Empty<Department>();
    }
}
