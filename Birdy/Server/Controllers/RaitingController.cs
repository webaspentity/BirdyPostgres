using Birdy.Server.AppData;
using Birdy.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Birdy.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RaitingController : Controller
{
    [HttpGet("getproductotalraiting/{productId}")]
    public async Task<IActionResult> GetProductTotalRaiting(int productId)
    {
        using (ApplicationDatabaseContext db = new ApplicationDatabaseContext())
        {
            var dbraitings = await db.Raitings.Where(r => r.ProductId == productId).ToListAsync();

            if (dbraitings is null || dbraitings.Count == 0)
            {
                return Ok(0);
            }
            else
            {
                double totalRaiting = Math.Round(((double)dbraitings.Sum(r => r.Value)) / dbraitings.Count(), 1);
                return Ok(totalRaiting);
            }
        }
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add(Raiting raiting)
    {
        using (ApplicationDatabaseContext db = new ApplicationDatabaseContext())
        {
            var dbraiting = await db.Raitings.FirstOrDefaultAsync(r => r.ProductId == raiting.ProductId && r.UserId == raiting.UserId);

            if (dbraiting is null)
            {
                var user = await db.Users.FirstOrDefaultAsync(u => u.Id == raiting.UserId);
                var product = await db.Products.FirstOrDefaultAsync(p => p.Id == raiting.ProductId);
                if (user is not null) raiting.User = user;
                if (product is not null) raiting.Product = product;
                db.Raitings.Add(raiting);
                await db.SaveChangesAsync();
                return Ok();
            }
            else
            {
                dbraiting.Value = raiting.Value;
                db.Entry(dbraiting);
                await db.SaveChangesAsync();
                return Ok();
            }
        }
    }
}
