using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;

namespace WebdevProjectStarterTemplate.Pages
{
    [Authorize]
    public class DisplayProducts : PageModel
    {
        public IEnumerable<Product> ProductWithCategory { get; set; } = null!;

        public void OnGet()
        {
            ProductWithCategory = new ProductRepository().GetProductWithCategory();
        }
    }
}