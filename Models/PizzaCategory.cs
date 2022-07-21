using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_razor_layout.Models
{
    public class PizzaCategory
    {

        public Pizza Pizza { get; set; }
        public List<Category>? Categories { get; set; }
       
        public List<SelectListItem>? Ingredients { get; set; }
        public List <string>? SelectedIngredients { get; set; }
    }
}
