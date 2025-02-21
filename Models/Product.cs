using System.ComponentModel.DataAnnotations;

namespace CRUDoperations.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; } "";

       
        public string Category { get; set; } "";

        
        public decimal Salary { get; set; }
        public DateTime CreatedAt { get; set; }
    }
} 
