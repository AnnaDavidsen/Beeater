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
    public class RatingsController : ControllerBase
    {
        private IRepositoryWrapper _repo;
        public RatingsController(IRepositoryWrapper repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rating>>> GetAll()
        {
            var entities = await _repo.Ratings.FindAll().ToListAsync();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Rating>> GetById(int id)
        {
            var entity = await _repo.Ratings.FindByCondition(x => x.Id == id).FirstOrDefaultAsync();

            if (entity != null)
                return Ok(entity);

            else
                return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] IEnumerable<Rating> entities)
        {
            _repo.Ratings.Create(entities);
            await _repo.SaveAsync();

            return Ok(entities);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] IEnumerable<Rating> entities)
        {
            _repo.Ratings.Update(entities);
            await _repo.SaveAsync();
            return Ok(entities);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var toDelete = await _repo.Ratings.FindByCondition(x => x.Id == id).ToListAsync();
            _repo.Ratings.Delete(toDelete);

            await _repo.SaveAsync();

            return Ok(toDelete);
        }

        [HttpGet("movie/{movieId}")]
        public async Task<ActionResult<IEnumerable<Rating>>> GetAllRatingsForMovie(int movieId)
        {
            var ratings = await _repo.Ratings.GetAllRatingsForMovie(movieId);

            return Ok(ratings);
        }

        [HttpGet("user/{userId}/movie/{movieId}")]
        public async Task<ActionResult<Rating>> GetRatingByUserAndMovieId(string userId, int movieId)
        {
            var rating = await _repo.Ratings.GetRatingByUserAndMovieId(userId, movieId);

            if (rating != null)
                return Ok(rating);

            else
                return NotFound();
                 
        }
    }
}
