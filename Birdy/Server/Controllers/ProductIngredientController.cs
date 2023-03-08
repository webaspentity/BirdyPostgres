using Birdy.Server.AppData;
using Birdy.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Birdy.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductIngredientController : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<ProductIngredient>> Get()
    {
        using (ApplicationDatabaseContext db = new ApplicationDatabaseContext())
        {
            var pi = await db.ProductIngredients.ToListAsync();
            return pi;
        }
    }
}