﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_razor_layout.Models
{
    public class PizzaCategory
    {

        public Pizza pizza { get; set; }
        public List<Category> categories { get; set; }
       
    }
}