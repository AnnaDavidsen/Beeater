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


        [HttpGet("movie/{movieId}")]
        public async Task<ActionResult<IEnumerable<Rating>>> GetAllRatingsForMovie(int movieId)
        {
            var ratings = await _repo.Ratings.FindByCondition(x => x.MovieId == movieId).ToListAsync();

            return ratings;
        }

        [HttpGet("user/{userId}/movie/{movieId}")]
        public async Task<ActionResult<Rating>> GetRatingByUserAndMovieId(string userId, int movieId)
        {
            var rating = await _repo.Ratings.FindByCondition(x => x.UserId == userId && x.MovieId == movieId)
                .FirstOrDefaultAsync();

            if (rating != null)
                return Ok(rating);

            else
                return NotFound();
                 
        }
    }
}
