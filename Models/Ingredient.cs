using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_razor_layout.Models
{
    [Table("ingredient")]
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        
        public List<Pizza> Pizzas { get; set; }
        
       
    }
}
