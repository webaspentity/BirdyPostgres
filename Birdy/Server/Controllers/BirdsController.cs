using Birdy.Server.AppData;
using Birdy.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Birdy.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BirdsController : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<Bird>> Get()
    {
        using (ApplicationDatabaseContext db = new ApplicationDatabaseContext())
        {
            var birds = await db.Birds.ToListAsync();
            return birds;
        }
    }
}