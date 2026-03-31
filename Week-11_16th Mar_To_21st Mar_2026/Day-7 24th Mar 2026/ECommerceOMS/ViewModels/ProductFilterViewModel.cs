using ECommerceOMS.Models;

namespace ECommerceOMS.ViewModels
{
    public class ProductFilterViewModel
    {
        public List<Product> Products { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
        public int? SelectedCategoryId { get; set; }
        public string? SearchTerm { get; set; }
    }
}