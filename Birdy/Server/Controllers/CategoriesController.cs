using Birdy.Server.AppData;
using Birdy.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Birdy.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<Category>> Get()
    {
        using (ApplicationDatabaseContext db = new ApplicationDatabaseContext())
        {
            var categories = await db.Categories.ToListAsync();
            return categories;
        }
    }
}
