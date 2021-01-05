using Beeater.Contracts;
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
    public class TheatersController : ControllerBase
    {
        private IRepositoryWrapper _repo;
        public TheatersController(IRepositoryWrapper repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Seat>>> GetAll()
        {
            var entities = await _repo.Seats.FindAll().ToListAsync();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Seat>> GetById(int id)
        {
            var entity = await _repo.Seats.FindByCondition(x => x.Id == id).FirstOrDefaultAsync();

            if (entity != null)
                return Ok(entity);

            else
                return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] IEnumerable<Seat> entities)
        {
            _repo.Seats.Create(entities);
            await _repo.SaveAsync();

            return Ok(entities);
        }


        [HttpGet("seats")]
        public async Task<ActionResult<IEnumerable<Theater>>> GetWithSeats()
        {
            return Ok(await _repo.Theaters
                .Include(t => t.Seats)
                .ToListAsync());
        }

        [HttpPut("seats")]
        public async Task<ActionResult<Theater>> PutTheaterWithSeats([FromBody] JObject theaterAndSeatsToBeDeleted)
        {
            Theater theater = theaterAndSeatsToBeDeleted["theater"].ToObject<Theater>();

            Seat[] seatsToBeDeleted = theaterAndSeatsToBeDeleted["seatsToBeDeleted"].ToObject<Seat[]>();
            _repo.Theaters.Update(new Theater[] { theater });
            _repo.Seats.Delete(seatsToBeDeleted);
            await _repo.SaveAsync();
            return Ok(theater);
        }



        [HttpPost("seats")]
        public async Task<ActionResult> PostTheaterWithSeats([FromBody] JObject theaterAndRows)
        {
            Theater theater = theaterAndRows["theater"].ToObject<Theater>();
            int[] rows = theaterAndRows["rows"].ToObject<int[]>();
            _repo.Theaters.Create(new Theater[] { theater });
            await _repo.SaveAsync();

            var seats = new List<Seat>();
            int row = 0;
            foreach (var seat in rows)
            {
                row++;
                for (int i = 1; i <= seat; i++)
                {
                    seats.Add(new Seat() { Number = i, Row = row, TheaterId = theater.Id });
                }
            }
            _repo.Seats.Create(seats);
            await _repo.SaveAsync();

            return Ok();
        }

        
    }
}
