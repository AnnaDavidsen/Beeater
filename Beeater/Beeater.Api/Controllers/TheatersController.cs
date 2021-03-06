﻿using Beeater.Contracts;
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
        public async Task<ActionResult<IEnumerable<Theater>>> GetAll()
        {
            var entities = await _repo.Theaters.FindAll().ToListAsync();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Theater>> GetById(int id)
        {
            var entity = await _repo.Theaters.FindByCondition(x => x.Id == id).FirstOrDefaultAsync();

            if (entity != null)
                return Ok(entity);

            else
                return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] IEnumerable<Theater> entities)
        {
            _repo.Theaters.Create(entities);
            await _repo.SaveAsync();

            return Ok(entities);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] IEnumerable<Theater> entities)
        {
            _repo.Theaters.Update(entities);
            await _repo.SaveAsync();
            return Ok(entities);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var toDelete = await _repo.Theaters.FindByCondition(x => x.Id == id).ToListAsync();
            var seatsToDelete = await _repo.Seats.FindByCondition(x => x.TheaterId == id).ToListAsync();
            _repo.Seats.Delete(seatsToDelete);
            _repo.Theaters.Delete(toDelete);

            await _repo.SaveAsync();

            return Ok(toDelete);
        }

        [HttpGet("seats")]
        public async Task<ActionResult<IEnumerable<Theater>>> GetWithSeats()
        {
            var theaters = await _repo.Theaters.GetAllTheatersWithSeats();
            return Ok(theaters);
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

            _repo.Theaters.CreateTheater(theater);
            await _repo.SaveAsync();

            _repo.Seats.CreateSeats(rows, theater.Id);
            await _repo.SaveAsync();

            return Ok();
        }

        
    }
}
