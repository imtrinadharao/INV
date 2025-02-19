using System.ComponentModel.DataAnnotations;

namespace CRUDoperations.Models
{
    public class ProductDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public decimal Salary { get; set; }
    }
}
