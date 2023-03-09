using Microsoft.AspNetCore.Mvc;
using Birdy.Shared;
using Birdy.Server.AppData;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace Birdy.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    [HttpPost("add")]
    public async Task<IActionResult> CreateOrder([FromBody] Order order)
    {
        using (ApplicationDatabaseContext db = new ApplicationDatabaseContext())
        {
            User? user = await db.Users.Include(u => u.Cart).FirstOrDefaultAsync(u => u.Id == order.UserId);


            if (user is not null)
            {
                int cartId = user.Cart.Id;
                order.User = user;
                db.Orders.Add(order);
                await db.SaveChangesAsync();

                var items = db.CartItems.Where(ci => ci.CartId == cartId);
                db.RemoveRange(items);
                await db.SaveChangesAsync();

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }

    [HttpGet("get/{userId}")]
    public async Task<IActionResult> GetProductsFromOrdersByUserId(int userId)
    {
        using (ApplicationDatabaseContext db = new ApplicationDatabaseContext())
        {
            var orders = await db.Orders.Include(o => o.ProductOrders).ThenInclude(po => po.Product).Where(o => o.UserId == userId).ToListAsync();

            if (orders is not null)
            {
                List<ProductOrder> productOrders = new();

                foreach (Order order in orders)
                {
                    if (order.ProductOrders is not null) productOrders.AddRange(order.ProductOrders);
                }

                if (productOrders.Count > 0)
                {
                    List<Product> products = new();

                    foreach (ProductOrder po in productOrders)
                    {
                        products.Add(po.Product);
                    }
                    return Ok(products);
                }
            }
            return BadRequest();
        }
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAll()
    {
        using (ApplicationDatabaseContext db = new ApplicationDatabaseContext())
        {
            //var orders = await db.Orders.Include(o => o.User).ThenInclude(u => u.Profile).Include(o => o.User).ThenInclude(u => u.LoginData).ToListAsync();
            var orders = await db.Orders.Include(o => o.User).ThenInclude(u => u.Profile).ToListAsync();

            if (orders is not null)
            {
                return Ok(orders);
            }
            else
            {
                return BadRequest();
            }
        }
    }

    [HttpPatch("edit/delivery/{order}")]
    public async Task<IActionResult> ChangeDeliveryDate([FromBody] Order order)
    {
        using (ApplicationDatabaseContext db = new ApplicationDatabaseContext())
        {
            var dborder = await db.Orders.FindAsync(order.Id);

            if (dborder is not null)
            {
                dborder.DeliveryDate = order.DeliveryDate;
                db.Entry(dborder);
                await db.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }

    [HttpPatch("edit/status/{order}")]
    public async Task<IActionResult> ChangeStatus([FromBody] Order order)
    {
        using (ApplicationDatabaseContext db = new ApplicationDatabaseContext())
        {
            var dborder = await db.Orders.FindAsync(order.Id);

            if (dborder is not null)
            {
                dborder.Status = order.Status;
                db.Entry(dborder);
                await db.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}