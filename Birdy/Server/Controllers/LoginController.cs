using Birdy.Server.AppData;
using Birdy.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Birdy.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] LoginData login)
    {
        using (ApplicationDatabaseContext db = new ApplicationDatabaseContext())
        {
            LoginData? dblogin = await db.Logins.FirstOrDefaultAsync(l => l.Email!.ToLower() == login.Email!.ToLower());

            if (dblogin is null)
            {
                return BadRequest("Указанный адрес электронной почты не зарегистрирован.");
            }
            else if (BCrypt.Net.BCrypt.Verify(login.Password, dblogin.Password))
            {
                var user = await db.Users.Include(u => u.LoginData).Include(u => u.Profile).Include(u => u.Cart).ThenInclude(c => c.Items).FirstOrDefaultAsync(u => u.Id == dblogin.UserId);
                string serializedUser = JsonSerializer.Serialize(user);
                return Ok(serializedUser);
            }
            else { return BadRequest("Пароль не соответствует указанному адресу электронной почты."); }
        }
    }
}