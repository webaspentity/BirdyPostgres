using Birdy.Server.AppData;
using Birdy.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Birdy.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{
    [HttpGet("{searchString}")]
    public async Task<IActionResult> Get(string searchString)
    {
        using (ApplicationDatabaseContext db = new ApplicationDatabaseContext())
        {
            string ss = searchString.ToLower();

            DateTime currentDate = DateTime.Now;
            var searchItems = await db.Products.Include(p => p.Category)
                .Where(p => p.Name.ToLower().Contains(ss) || p.Manufacturer.ToLower().Contains(ss) || p.Description.ToLower().Contains(ss))
                .Include(p => p.ProductsPrices)
                .ThenInclude(pp => pp.Discounts.Where(d => d.StartDate <= currentDate && d.EndDate >= currentDate)).ToListAsync();

            if (searchItems is not null) return Ok(searchItems);
            else return BadRequest();
        }
    }
}