using la_mia_pizzeria_razor_layout.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace la_mia_pizzeria_razor_layout.Controllers
{
    public class PizzaController : Controller
    {
        public IActionResult Index()
        {
            using (PizzaContext db = new PizzaContext())
            {
                List<Pizza> pizzas = db.Pizza.ToList();
                return View(pizzas);
            }
        }
        public IActionResult Detail(int id)
        {
            using(PizzaContext db = new PizzaContext())
            {
                Pizza pizzaFound = db.Pizza.Where(pizza => pizza.Id == id).FirstOrDefault();
                if(pizzaFound == null)
                {
                    return View("Error");
                }
                else
                {
                return View("Detail",pizzaFound);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza pizza)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateForm", pizza);
            }
            using(PizzaContext context = new PizzaContext()) 
            {
                context.Add(new Pizza(pizza.Name, pizza.Description, pizza.Image, pizza.Price));
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            
        }

        [HttpGet]
        public IActionResult CreateForm()
        {
            return View();

        }
    }
}
