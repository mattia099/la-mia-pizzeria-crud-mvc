using la_mia_pizzeria_razor_layout.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace la_mia_pizzeria_razor_layout.Controllers
{
    public class EserciziRazor : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

    }
}