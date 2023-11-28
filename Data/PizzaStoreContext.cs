using Microsoft.EntityFrameworkCore;

namespace BlazingPizza.Data;

public class PizzaStoreContext : DbContext
{
    public DbSet<PizzaSpecial> Specials { get; set; }
    public DbSet<Pizza> Pizzas { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Topping> Toppings { get; set; }
    public PizzaStoreContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //* configure many to many relations 
        modelBuilder.Entity<PizzaTopping>()
            .HasKey(pst => new { pst.PizzaId, pst.ToppingId });

        modelBuilder.Entity<PizzaTopping>()
            .HasOne<Pizza>()
            .WithMany(pst => pst.Toppings);

        modelBuilder.Entity<PizzaTopping>()
            .HasOne(pst => pst.Topping)
            .WithMany();
    }
}
