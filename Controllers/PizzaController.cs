using la_mia_pizzeria_razor_layout.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                Pizza pizzaFound = db.Pizza.Where(pizza => pizza.Id == id).Include(pizza => pizza.Category).FirstOrDefault();
                if(pizzaFound == null)
                {
                    return NotFound("Nessun prodotto con questo id");
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
                context.Add(pizza);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            
        }

        [HttpGet]
        public IActionResult CreateForm()
        {
            using(PizzaContext cxt = new PizzaContext())
            {
                ViewData["categories"] = cxt.Category.ToList();
            }
            return View();

        }

        [HttpGet]
        
        public IActionResult Edit(int id)
        {
            using(PizzaContext cxt=new PizzaContext())
            {
                ViewData["categories"] = cxt.Category.ToList();
                Pizza pizza = cxt.Pizza.Where(p => p.Id == id).FirstOrDefault();
                if (pizza == null)
                {
                    return NotFound();
                }
                return View(pizza);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Pizza pizza)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", pizza);
            }

            using(PizzaContext cxt = new PizzaContext())
            {
                Pizza toModify = cxt.Pizza.Where(p => p.Id == id).FirstOrDefault();
                if(toModify != null)
                {
                    toModify.Name = pizza.Name;
                    toModify.Description = pizza.Description;
                    toModify.Image = pizza.Image;
                    toModify.Price = pizza.Price;
                    toModify.CategoryID = pizza.CategoryID;
                    cxt.SaveChanges();

                    return RedirectToAction("Index");
                }

                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryTokenAttribute]
        public IActionResult Delete(int id)
        {
            using(PizzaContext cxt = new PizzaContext())
            {
                Pizza toDelete = cxt.Pizza.Where(p => p.Id == id).FirstOrDefault();
                if(toDelete != null)
                {
                    cxt.Remove(toDelete);
                    cxt.SaveChanges();
                    return RedirectToAction("Index");
                }
                return NotFound();
                
            }
        }
    }
            

}
