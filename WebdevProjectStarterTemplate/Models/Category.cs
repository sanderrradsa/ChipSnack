using System.ComponentModel.DataAnnotations;

namespace WebdevProjectStarterTemplate.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required, MinLength(2), MaxLength(128)]
        public string Name { get; set; } = null!;

        public List<Product?> Products { get; set; } = new List<Product?>();
    }
}
