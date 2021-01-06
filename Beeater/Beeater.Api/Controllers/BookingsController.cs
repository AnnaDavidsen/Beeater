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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetAll()
        {
            var entities = await _repo.Bookings.FindAll().ToListAsync();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetById(int id)
        {
            var entity = await _repo.Bookings.FindByCondition(x => x.Id == id).FirstOrDefaultAsync();

            if (entity != null)
                return Ok(entity);

            else
                return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] IEnumerable<Booking> entities)
        {
            _repo.Bookings.Create(entities);
            await _repo.SaveAsync();

            return Ok(entities);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] IEnumerable<Booking> entities)
        {
            _repo.Bookings.Update(entities);
            await _repo.SaveAsync();
            return Ok(entities);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var toDelete = await _repo.Bookings.FindByCondition(x => x.Id == id).ToListAsync();
            _repo.Bookings.Delete(toDelete);

            await _repo.SaveAsync();

            return Ok(toDelete);
        }

        [HttpGet("{id}/detailed")]
        public async Task<ActionResult<Booking>> GetBookingDetailed(int id)
        {
            var booking = await _repo.Bookings.GetBookingDetailed(id);

            return Ok(booking);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookingsForUser(string userId)
        {
            var bookings = await _repo.Bookings.GetBookingsForUser(userId);
                
            return Ok(bookings);
        }
    }
}
