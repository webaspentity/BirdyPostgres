using Birdy.Server.AppData;
using Birdy.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Birdy.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    [HttpPost("add")]
    public async Task<IActionResult> AddItem([FromBody] CartItem item)
    {
        using (ApplicationDatabaseContext db = new ApplicationDatabaseContext())
        {
            ProductPrice pp = await db.ProductPrices.FirstAsync(pp => pp.ProductId == item.ProductId && pp.Weight == item.Weight);
            Discount? discount = await db.Discounts.FirstOrDefaultAsync(d => d.ProductPriceId == pp.Id);
            Birdy.Shared.Cart? cart = await db.Carts.FindAsync(item.CartId);

            CartItem newItem = new();
            newItem.ProductId = item.ProductId;
            newItem.Quantity = 1;

            if (discount is not null)
            {
                newItem.Price = pp.Price - pp.Price * discount.Value / 100;
            }
            else
            {
                newItem.Price = pp.Price;
            }

            newItem.Weight = item.Weight;
            newItem.CartId = item.CartId;

            db.CartItems.Add(newItem);
            await db.SaveChangesAsync();

            var totalPrice = await db.CartItems.Where(ci => ci.CartId == cart.Id).SumAsync(ci => ci.Price);
            cart.TotalCost = totalPrice;
            db.Entry(cart);
            await db.SaveChangesAsync();

            return Ok(newItem);
        }
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteItem(int id)
    {
        using (ApplicationDatabaseContext db = new ApplicationDatabaseContext())
        {
            CartItem? item = await db.CartItems.FindAsync(id);
            Birdy.Shared.Cart? cart = await db.Carts.FindAsync(item.CartId);

            db.CartItems.Remove(item);
            await db.SaveChangesAsync();

            var totalPrice = await db.CartItems.Where(ci => ci.CartId == cart.Id).SumAsync(ci => ci.Price);
            cart.TotalCost = totalPrice;
            db.Entry(cart);
            await db.SaveChangesAsync();

            return Ok();
        }
    }

    [HttpPatch("increment/{id}")]
    public async Task<IActionResult> IncrementQuantity(int id)
    {
        using (ApplicationDatabaseContext db = new ApplicationDatabaseContext())
        {
            CartItem? item = await db.CartItems.FindAsync(id);
            ProductPrice pp = await db.ProductPrices.FirstAsync(pp => pp.ProductId == item.ProductId && pp.Weight == item.Weight);
            Discount? discount = await db.Discounts.FirstOrDefaultAsync(d => d.ProductPriceId == pp.Id);
            Birdy.Shared.Cart? cart = await db.Carts.FindAsync(item.CartId);

            if (pp.InStock > item.Quantity)
            {
                item.Quantity += 1;
            }
            else
            {
                return BadRequest();
            }

            if (discount is not null)
            {
                item.Price = (pp.Price - pp.Price * discount.Value / 100) * item.Quantity;
            }
            else
            {
                item.Price = pp.Price * item.Quantity;
            }

            db.Entry(item);
            await db.SaveChangesAsync();

            var totalPrice = await db.CartItems.Where(ci => ci.CartId == cart.Id).SumAsync(ci => ci.Price);
            cart.TotalCost = totalPrice;
            db.Entry(cart);
            await db.SaveChangesAsync();

            return Ok();
        }
    }

    [HttpPatch("decrement/{id}")]
    public async Task<IActionResult> DecrementQuantity(int id)
    {
        using (ApplicationDatabaseContext db = new ApplicationDatabaseContext())
        {
            CartItem? item = await db.CartItems.FindAsync(id);
            ProductPrice pp = await db.ProductPrices.FirstAsync(pp => pp.ProductId == item.ProductId && pp.Weight == item.Weight);
            Discount? discount = await db.Discounts.FirstOrDefaultAsync(d => d.ProductPriceId == pp.Id);
            Birdy.Shared.Cart? cart = await db.Carts.FindAsync(item.CartId);

            item.Quantity -= 1;

            if (discount is not null)
            {
                item.Price = (pp.Price - pp.Price * discount.Value / 100) * item.Quantity;
            }
            else
            {
                item.Price = pp.Price * item.Quantity;
            }

            db.Entry(item);
            await db.SaveChangesAsync();

            var totalPrice = await db.CartItems.Where(ci => ci.CartId == cart.Id).SumAsync(ci => ci.Price);
            cart.TotalCost = totalPrice;
            db.Entry(cart);
            await db.SaveChangesAsync();

            return Ok();
        }
    }

    [HttpGet("total/{cartId}")]
    public async Task<IActionResult> GetTotalCost(int cartId)
    {
        using (ApplicationDatabaseContext db = new ApplicationDatabaseContext())
        {
            decimal total = await db.CartItems.Where(ci => ci.CartId == cartId).SumAsync(ci => ci.Price);
            return Ok(total);
        }
    }

    [HttpGet("get/{cartId}")]
    public async Task<IActionResult> GetItemsByCartId(int cartId)
    {
        using (ApplicationDatabaseContext db = new ApplicationDatabaseContext())
        {
            List<CartItem>? items = await db.CartItems.Where(ci => ci.CartId == cartId).ToListAsync();

            if (items is not null)
            {
                foreach (CartItem ci in items)
                {
                    ProductPrice? pp = await db.ProductPrices.Include(pp => pp.Discounts).FirstOrDefaultAsync(pp => pp.ProductId == ci.ProductId && pp.Weight == ci.Weight);
                    
                    if (pp is not null)
                    {
                        Discount? d = pp?.Discounts?.FirstOrDefault(d => d.StartDate <= DateTime.Now && d.EndDate >= DateTime.Now);

                        if (d is not null)
                        {
                            ci.Price = (pp.Price - pp.Price * d.Value / 100) * ci.Quantity;
                        }
                        else
                        {
                            ci.Price = pp.Price * ci.Quantity;
                        }
                    }
                }
                return Ok(items);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}