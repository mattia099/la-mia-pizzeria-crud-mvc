using la_mia_pizzeria_razor_layout.Models;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_razor_layout
{
    public class PizzaContext : DbContext
    {
        public DbSet<Pizza> Pizza { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=db-pizzeria;Integrated Security=True;Pooling=False");
        }
    }
}
