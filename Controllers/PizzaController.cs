using la_mia_pizzeria_razor_layout.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        [HttpGet]
        public IActionResult CreateForm()
        {
            using (PizzaContext db = new PizzaContext())
            {
                PizzaCategory model = new PizzaCategory();
                model.Pizza = new Pizza();
                List<Category> categories = db.Category.ToList();
                model.Categories = categories;

                List<SelectListItem> IngredientList = new List<SelectListItem>();
                List<Ingredient> Ingredients = db.Ingredients.ToList();

                foreach (Ingredient ing in Ingredients) 
                {
                    IngredientList.Add(new SelectListItem() { Text = ing.Name, Value = ing.Id.ToString() });
                }

                model.Ingredients = IngredientList;

                return View("CreateForm", model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaCategory data)
        {
            if (!ModelState.IsValid)
            {
                using (PizzaContext context = new PizzaContext())
                {
                    List<Category> categories = context.Category.ToList();
                    data.Categories= categories;

                    List<SelectListItem> IngredientList = new List<SelectListItem>();
                    List<Ingredient> Ingredients = context.Ingredients.ToList();

                    foreach (Ingredient ing in Ingredients)
                    {
                        IngredientList.Add(new SelectListItem() { Text = ing.Name, Value = ing.Id.ToString() });
                    }

                    return View("CreateForm", data);
                }
                
            }
            
            using(PizzaContext db = new PizzaContext())
            {
                Pizza postToCreate = new Pizza();
                postToCreate.Name = data.Pizza.Name;
                postToCreate.Description = data.Pizza.Description;
                postToCreate.Image = data.Pizza.Image;
                postToCreate.Price = data.Pizza.Price;
                postToCreate.CategoryID = data.Pizza.CategoryID;
                postToCreate.Ingredients = new List<Ingredient>();

                if(data.SelectedIngredients != null) 
                {
                    foreach(string selectedIngId in data.SelectedIngredients)
                    {
                        int selectedIntIngId = int.Parse(selectedIngId);
                        Ingredient ingredient = db.Ingredients.Where(i => i.Id == selectedIntIngId).FirstOrDefault();
                        postToCreate.Ingredients.Add(ingredient);
                    }
                }

                db.Pizza.Add(postToCreate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            
        }  
                
        
        [HttpGet]
        public IActionResult Edit(int id)
        {
            using(PizzaContext cxt=new PizzaContext())
            {
                PizzaCategory model = new PizzaCategory();
                List<Category> categories = cxt.Category.ToList();
                model.Categories = categories;
                model.Pizza = cxt.Pizza.Where(p => p.Id == id).FirstOrDefault();
                if (model.Pizza == null)
                {
                    return NotFound();
                }
                return View(model);
            }
        }
                

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, PizzaCategory data)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", data);
            }

            using(PizzaContext cxt = new PizzaContext())
            {
                Pizza toModify = cxt.Pizza.Where(p => p.Id == id).FirstOrDefault();
                if(toModify != null)
                {
                    toModify.Name = data.Pizza.Name;
                    toModify.Description = data.Pizza.Description;
                    toModify.Image = data.Pizza.Image;
                    toModify.Price = data.Pizza.Price;
                    toModify.CategoryID = data.Pizza.CategoryID;
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
