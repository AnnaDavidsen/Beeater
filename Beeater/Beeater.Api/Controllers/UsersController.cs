using Beeater.Domain.Entities;
using Beeater.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beeater.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : CommonController<User>
    {
        public UsersController(beeaterContext context)
            : base(context)
        {

        }
        // GetAll, GetById and Post are located in CommonController class

        [HttpGet("{firstName}/{lastName}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersByFullName(string firstName, string lastName)
        {
            var users = await _context.Users
                .Where(x => x.Firstname.ToLower() == firstName.ToLower()
                    && x.Lastname.ToLower() == lastName.ToLower())
                .ToListAsync();

            return Ok(users);
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<User>> GetUserByEmail(string email)
        {
            var user = await _context.Users
                .Include(x => x.Bookings)
                .FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());

            return Ok(user);
        }

        [HttpGet("points/{minPoints}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersWithPointsGreaterThan(int minpoints)
        {
            var users = await _context.Users.Where(x => x.BonusPoints > minpoints).ToListAsync();

            return Ok(users);
        }
    }
}
