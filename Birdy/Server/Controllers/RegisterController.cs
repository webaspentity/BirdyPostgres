using Birdy.Server.AppData;
using Birdy.Shared;
using Birdy.Shared.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Birdy.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegisterController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] LoginData login)
    {
        using (ApplicationDatabaseContext db = new ApplicationDatabaseContext())
        {
            LoginData? dblogin = await db.Logins.FirstOrDefaultAsync(l => l.Email!.ToLower() == login.Email!.ToLower());

            if (dblogin is null)
            {
                string hash = BCrypt.Net.BCrypt.HashPassword(login.Password);

                dblogin = new LoginData
                {
                    Email = login.Email,
                    Password = hash
                };

                Profile newProfile = new Profile();

                Cart newCart = new Cart();

                User newUser = new User
                {
                    LoginData = dblogin,
                    Profile = newProfile,
                    Role = UserRole.User,
                    Cart = newCart
                };

                dblogin.User = newUser;
                newProfile.User = newUser;
                newCart.User = newUser;

                db.Logins.Add(dblogin);
                db.Profiles.Add(newProfile);
                db.Carts.Add(newCart);
                db.Users.Add(newUser);
                db.SaveChanges();

                var user = await db.Users.Include(u => u.LoginData).Include(u => u.Profile).Include(u => u.Cart).ThenInclude(c => c.Items).FirstOrDefaultAsync(u => u.LoginData.Email!.ToLower() == login.Email!.ToLower());
                string serializedUser = JsonSerializer.Serialize(user);
                return Ok(serializedUser);
            }
            else if (BCrypt.Net.BCrypt.Verify(login.Password, dblogin.Password))
            {
                var user = await db.Users.Include(u => u.LoginData).Include(u => u.Profile).Include(u => u.Cart).ThenInclude(c => c.Items).FirstOrDefaultAsync(u => u.Id == dblogin.UserId);
                string serializedUser = JsonSerializer.Serialize(user);
                return Ok(serializedUser);
            }
            else { return BadRequest("Указанный адрес электронной почты уже зарегистрирован, но пароли не совпадают."); }
        }
    }
}