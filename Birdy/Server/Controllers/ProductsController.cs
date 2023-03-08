using Birdy.Server.AppData;
using Birdy.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Birdy.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        using (ApplicationDatabaseContext db = new ApplicationDatabaseContext())
        {
            var products = await db.Products.Include(p => p.Category).Include(p => p.ProductsPrices).ThenInclude(pp => pp.Discounts).ToListAsync();
            return products;
        }
    }

    [HttpGet("{category}")]
    public async Task<IEnumerable<Product>> GetProductsByCategory(string category)
    {
        using (ApplicationDatabaseContext db = new ApplicationDatabaseContext())
        {
            DateTime currentDate = DateTime.Now;

            var products = await db.Products.
                Include(p => p.Category).Where(p => p.Category.Url.Equals(category)).
                Include(p => p.ProductsPrices).ThenInclude(pp => pp.Discounts.Where(d => d.StartDate <= currentDate && d.EndDate >= currentDate)).
                ToListAsync();
            return products;
        }
    }

    [HttpGet("getbyid/{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        using (ApplicationDatabaseContext db = new ApplicationDatabaseContext())
        {
            Product? product = await db.Products.Include(p => p.Category).Include(p => p.ProductsPrices).ThenInclude(pp => pp.Discounts).FirstOrDefaultAsync(p => p.Id == id);

            if (product is not null) return Ok(product);
            else return BadRequest();
        }
    }
}