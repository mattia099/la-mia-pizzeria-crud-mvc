using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_razor_layout.Models
{
    [Table("pizza")]
    public class Pizza
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required]
        public string Image { get; set; }
        
        [Required]
        [Range(1,30)]
        public float Price { get; set; }
        
        public int? CategoryID { get; set; }
        public Category? Category { get; set; }
        
        public List<Ingredient>? Ingredients { get; set; }

        public Pizza()
        {
        }

        public Pizza(string name, string description, string image, float price)
        {
            Name = name;
            Description = description;
            Image = image;
            Price = price;
        }
    }
}
