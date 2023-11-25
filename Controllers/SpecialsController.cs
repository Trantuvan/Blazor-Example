
using BlazingPizza.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazingPizza.Controllers;

[Route("specials")]
[ApiController]
public class SpecialsController : Controller
{
    private readonly PizzaStoreContext _db;

    public SpecialsController(PizzaStoreContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PizzaSpecial>>> GetSpecials()
    {
        var specials = (await _db.Specials.ToListAsync()).OrderByDescending(s => s.BasePrice);
        return Ok(specials);
    }
}
