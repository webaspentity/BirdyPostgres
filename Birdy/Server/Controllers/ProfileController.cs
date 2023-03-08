using Microsoft.AspNetCore.Mvc;
using Birdy.Shared;
using Birdy.Server.AppData;
using Microsoft.EntityFrameworkCore;

namespace Birdy.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfileController : ControllerBase
{
    [HttpPatch]
    public async Task<IActionResult> UpdateProfile([FromBody] User updatedUser)
    {
        using (ApplicationDatabaseContext db = new ApplicationDatabaseContext())
        {
            User? userWithTheSameEmail = await db.Users.FirstOrDefaultAsync(u => u.LoginData.Email.ToLower() == updatedUser.LoginData.Email.ToLower());
            User? originalUser = await db.Users.Include(u => u.LoginData).Include(u => u.Profile).Include(u => u.Cart).FirstOrDefaultAsync(u => u.Id == updatedUser.Id);

            if (userWithTheSameEmail is not null && userWithTheSameEmail.Id != updatedUser.Id)
            {
                return BadRequest("Указанный адрес электронной почты уже используется.");
            }

            if (originalUser is not null)
            {
                originalUser.LoginData.Email = updatedUser.LoginData.Email;
                originalUser.Profile.UserName = updatedUser.Profile.UserName;
                originalUser.Profile.UserAddress = updatedUser.Profile.UserAddress;
                originalUser.Profile.UserTelephone = updatedUser.Profile.UserTelephone;

                if (!String.IsNullOrEmpty(updatedUser.LoginData.Password))
                {
                    originalUser.LoginData.Password = BCrypt.Net.BCrypt.HashPassword(updatedUser.LoginData.Password);
                }

                db.Entry(originalUser);
                db.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
