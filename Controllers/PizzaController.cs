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

                    List<SelectListItem> IngredientList = new List<SelectListItem>(); //isanzio lista di elementi selezionati
                    List<Ingredient> Ingredients = context.Ingredients.ToList(); //Lista ingredienti del db

                    foreach (Ingredient ing in Ingredients) //ciclo ingredienti del db
                    {
                        IngredientList.Add(new SelectListItem() { Text = ing.Name, Value = ing.Id.ToString() }); //passo al costruttore il contenuto della select e il value
                    }

                    return View("CreateForm", data);
                }
                
            }
            
            using(PizzaContext db = new PizzaContext())
            {
                Pizza pizzaToCreate = new Pizza();
                pizzaToCreate.Name = data.Pizza.Name;
                pizzaToCreate.Description = data.Pizza.Description;
                pizzaToCreate.Image = data.Pizza.Image;
                pizzaToCreate.Price = data.Pizza.Price;
                pizzaToCreate.CategoryID = data.Pizza.CategoryID;
                pizzaToCreate.Ingredients = new List<Ingredient>(); //definisco e istanzio lista di ingredienti del db

                if(data.SelectedIngredients != null) 
                {
                    foreach(string selectedIngId in data.SelectedIngredients)
                    {
                        int selectedIntIngId = int.Parse(selectedIngId);
                        Ingredient ingredient = db.Ingredients.Where(i => i.Id == selectedIntIngId).FirstOrDefault();
                        pizzaToCreate.Ingredients.Add(ingredient);
                    }
                }
                db.Pizza.Add(pizzaToCreate);
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

                List<SelectListItem> IngredientList = new List<SelectListItem>();
                List<Ingredient> Ingredients = cxt.Ingredients.ToList();

                foreach (Ingredient ing in Ingredients)
                {
                    IngredientList.Add(new SelectListItem() { Text = ing.Name, Value = ing.Id.ToString() });
                }

                model.Ingredients = IngredientList;

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
                Pizza toModify = cxt.Pizza.Where(p => p.Id == id).Include(p=>p.Ingredients).FirstOrDefault();
                if(toModify != null)
                {
                    toModify.Name = data.Pizza.Name;
                    toModify.Description = data.Pizza.Description;
                    toModify.Image = data.Pizza.Image;
                    toModify.Price = data.Pizza.Price;
                    toModify.CategoryID = data.Pizza.CategoryID;

                    toModify.Ingredients.Clear();
                    List<SelectListItem> ingredientList = new List<SelectListItem>();
                    if (data.SelectedIngredients != null)
                    {
                        foreach (string selectedIngId in data.SelectedIngredients)
                        {
                            int selectedIntIngId = int.Parse(selectedIngId);
                            Ingredient ingredient = cxt.Ingredients.Where(i => i.Id == selectedIntIngId).FirstOrDefault();

                            toModify.Ingredients.Add(ingredient);
                        }
                    }


                    cxt.Update(toModify);
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
