using BlazingPizza.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazingPizza.Controllers;

[Route("[controller]")]
[ApiController]
public class OrdersController : Controller
{
    private readonly PizzaStoreContext _db;

    public OrdersController(PizzaStoreContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<List<OrderWithStatus>>> GetOrders()
    {
        var orders = await _db.Orders
            .Include(o => o.Pizzas)
            .ThenInclude(p => p.Special)
            .Include(o => o.Pizzas)
            .ThenInclude(p => p.Toppings)
            .ThenInclude(t => t.Topping)
            .OrderByDescending(o => o.CreatedTime)
            .AsSplitQuery()
            .ToListAsync();

        return orders.Select(o => OrderWithStatus.FromOrder(o)).ToList();
    }

    [HttpGet("{orderId}")]
    public async Task<ActionResult<OrderWithStatus>> GetOrderWithStatus(int orderId)
    {
        var order = await _db.Orders
            .Where(o => o.OrderId == orderId)
            .Include(o => o.Pizzas)
            .ThenInclude(p => p.Special)
            .Include(o => o.Pizzas)
            .ThenInclude(p => p.Toppings)
            .ThenInclude(pt => pt.Topping)
            .FirstOrDefaultAsync();

        if (order is null)
        {
            return NotFound();
        }
        return OrderWithStatus.FromOrder(order);
    }


    [HttpPost]
    public async Task<ActionResult<int>> PlaceOrder([FromBody] Order order)
    {
        order.CreatedTime = DateTime.Now;
        foreach (var pizza in order.Pizzas)
        {
            pizza.SpecialId = pizza.Special.Id;
            pizza.Special = default;
        }
        _db.Orders.Attach(order);
        await _db.SaveChangesAsync();

        return order.OrderId;
    }
}
