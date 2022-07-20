using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_razor_layout.Models
{
    public class PizzaIngredients
    {

        public Pizza Pizza { get; set; }
        public string Ingredients { get; set; }
       
    }
}
