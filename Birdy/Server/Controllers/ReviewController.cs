using Birdy.Server.AppData;
using Birdy.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Birdy.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewController : ControllerBase
{
    [HttpGet("getbyproductid/{productId}")]
    public async Task<IActionResult> GetReviewByProductId(int productId)
    {
        using (ApplicationDatabaseContext db = new ApplicationDatabaseContext())
        {
            var reviews = await db.Reviews.Where(r => r.ProductId == productId).ToListAsync();
            
            if (reviews is not null)
            {
                List<ReviewModel> models = new List<ReviewModel>();

                foreach (Review r in reviews)
                {
                    ReviewModel model = new ReviewModel();
                    User? user = await db.Users.Include(u => u.Profile).FirstOrDefaultAsync(u => u.Id == r.UserId);
                    model.UserName = user?.Profile.UserName ?? "Аноним";
                    model.Comment = r.Comment;
                    models.Add(model);
                }
                return Ok(models);
            }
            else
            {
                return BadRequest("Отзывы не найдены.");
            }
        }
    }

    [HttpPost("save")]
    public async Task<IActionResult> Save(Review newReview)
    {
        using (ApplicationDatabaseContext db = new ApplicationDatabaseContext())
        {
            var dbreview = await db.Reviews.FirstOrDefaultAsync(r => r.ProductId == newReview.ProductId && r.UserId == newReview.UserId);

            if (dbreview is null)
            {
                User? user = await db.Users.FirstOrDefaultAsync(u => u.Id == newReview.UserId);
                if (user is not null) newReview.User = user;
                Product? product = await db.Products.FirstOrDefaultAsync(p => p.Id == newReview.ProductId);
                if (product is not null) newReview.Product = product;
                db.Reviews.Add(newReview);
                await db.SaveChangesAsync();
                return Ok();
            }
            else
            {
                dbreview.Comment = newReview.Comment;
                dbreview.ReviewDate = newReview.ReviewDate;
                db.Entry(dbreview);
                await db.SaveChangesAsync();
                return Ok();
            }
        } 
    }
}
