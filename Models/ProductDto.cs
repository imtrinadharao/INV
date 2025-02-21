using System.ComponentModel.DataAnnotations;

namespace CRUDoperations.Models
{
    public class ProductDto
    {
        [Required]
        public string Name { get; set; }= "";
      
        public string Category { get; set; }= "";

        public decimal Salary { get; set; }
    }
}
