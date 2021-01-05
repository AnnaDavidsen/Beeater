using Beeater.Contracts;
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
    public class BookingsController : ControllerBase
    {
        private IRepositoryWrapper _repo;
        public BookingsController(IRepositoryWrapper repo)
        {
            _repo = repo;
        }
        // GetAll, GetById and Post are located in CommonController class
        
        [HttpGet("{id}/detailed")]
        public async Task<ActionResult<Booking>> GetBookingDetailed(int id)
        {
            var booking = await _repo.Bookings
                .Include(x => x.Seat, x => x.Show)
                .FirstOrDefaultAsync(x => x.Id == id);
            //var booking = await _context.Bookings
            //    .Include(x => x.Seat)
            //    .Include(x => x.Show)
            //    .FirstOrDefaultAsync(x => x.Id == id);

            return Ok(booking);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookingsForUser(string userId)
        {
            var bookings = await _repo.Bookings
                .Include(x => x.Show, x => x.Show.Movie, x => x.Show.Theater, x => x.Seat)
                .Where(x => x.UserId == userId)
                .ToListAsync();
                
            //var bookings = await _context.Bookings
            //    .Include(x => x.Show)
            //        .ThenInclude(x => x.Movie)
            //    .Include(x => x.Show)
            //        .ThenInclude(x => x.Theater)
            //    .Include(x => x.Seat)                    
            //     .ToListAsync();

            return Ok(bookings);
        }
    }
}
