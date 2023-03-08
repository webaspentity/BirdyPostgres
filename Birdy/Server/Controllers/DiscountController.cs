using Birdy.Server.AppData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Birdy.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DiscountController : Controller
{
    [HttpGet("getbyppid/{ppId}")]
    public async Task<IActionResult> GetDiscountByProductPriceId(int ppId)
    {
        using (ApplicationDatabaseContext db = new ApplicationDatabaseContext())
        {
            var discount = await db.Discounts.Where(d => d.ProductPriceId == ppId && d.StartDate <= DateTime.Now && d.EndDate >= DateTime.Now).FirstOrDefaultAsync();
            
            if (discount is not null)
            {
                return Ok(discount);
            }
            else
            {
                return Ok("null");
            }
        }
    }
}
