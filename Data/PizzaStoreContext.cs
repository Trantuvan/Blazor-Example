using Microsoft.EntityFrameworkCore;

namespace BlazingPizza;

public class PizzaStoreContext : DbContext
{
    public DbSet<PizzaSpecial> Specials { get; set; }
    public PizzaStoreContext(DbContextOptions options) : base(options) { }
}
