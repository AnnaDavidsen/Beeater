using Beeater.Domain.Entities;
using Beeater.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beeater.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TheatersController : CommonController<Theater>
    {
        public TheatersController(beeaterContext context)
            : base(context)
        {

        }

        [HttpGet("seats")]
        public async Task<ActionResult<IEnumerable<Theater>>> GetWithSeats()
        {
            return Ok(await _context.Theaters.Include(t => t.Seats).ToListAsync());
        }

        [HttpPut("seats")]
        public async Task<ActionResult> PutTheaterWithSeats([FromBody] JObject theaterAndRows)
        {
            Theater theater = theaterAndRows["theater"].ToObject<Theater>();
            Seat[] seats = theaterAndRows["seats"].ToObject<Seat[]>();
            return Ok();
        }



        [HttpPost("seats")]
        public async Task<ActionResult> PostTheaterWithSeats([FromBody] JObject theaterAndRows)
        {
            Theater theater = theaterAndRows["theater"].ToObject<Theater>();
            int[] rows = theaterAndRows["rows"].ToObject<int[]>();
            _context.Theaters.Add(theater);
            await _context.SaveChangesAsync();

            int row = 0;
            foreach (var seats in rows)
            {
                row++;
                for (int i = 1; i <= seats; i++)
                {
                    _context.Seats.Add(new Seat { Number = i, Row = row, TheaterId = theater.Id });
                }
            }

            await _context.SaveChangesAsync();

            return Ok();
        }

        
    }
}
