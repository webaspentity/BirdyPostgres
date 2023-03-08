using Birdy.Server.AppData;
using Birdy.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Birdy.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientsController : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<Ingredient>> Get()
    {
        using (ApplicationDatabaseContext db = new ApplicationDatabaseContext())
        {
            var ingredients = await db.Ingredients.Include(i => i.Products).ToListAsync();
            return ingredients;
        }
    }
}