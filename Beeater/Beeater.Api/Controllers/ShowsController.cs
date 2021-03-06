﻿using Beeater.Contracts;
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
    public class ShowsController : ControllerBase
    {
        private IRepositoryWrapper _repo;
        public ShowsController(IRepositoryWrapper repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Show>>> GetAll()
        {
            var entities = await _repo.Shows.FindAll().ToListAsync();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Show>> GetById(int id)
        {
            var entity = await _repo.Shows.FindByCondition(x => x.Id == id).FirstOrDefaultAsync();

            if (entity != null)
                return Ok(entity);

            else
                return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] IEnumerable<Show> entities)
        {
            _repo.Shows.Create(entities);
            await _repo.SaveAsync();

            return Ok(entities);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] IEnumerable<Show> entities)
        {
            _repo.Shows.Update(entities);
            await _repo.SaveAsync();
            return Ok(entities);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var toDelete = await _repo.Shows.FindByCondition(x => x.Id == id).ToListAsync();
            _repo.Shows.Delete(toDelete);

            await _repo.SaveAsync();

            return Ok(toDelete);
        }

        [HttpGet("movieid/{movieid}")]
        public async Task<ActionResult<IEnumerable<Show>>> GetShowByMovieId(int movieId)
        {
            var shows = await _repo.Shows.GetShowsByMovieId(movieId);

            return Ok(shows);
        }

        [HttpGet("title/{title}")]
        public async Task<ActionResult<IEnumerable<Show>>> GetShowByMovieTitle(string title)
        {
            var shows = await _repo.Shows.GetShowsByMovieTitle(title);

            return Ok(shows);
        }
        [HttpGet("theater/{theaterId}")]
        public async Task<ActionResult<IEnumerable<Show>>> GetShowsByTheaterId(int theaterId)
        {
            var shows = await _repo.Shows.GetShowsByTheaterId(theaterId);

            return Ok(shows);
        }

        [HttpGet("seatstatus/{showId}")]
        public async Task<ActionResult<object>> GetShowWithSeatsAndSeatStatus(int showId)
        {
            var show = await _repo.Shows.GetShowWithSeatsAndSeatStatus(showId);

            if (show!=null)
            {
                return Ok(show);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
