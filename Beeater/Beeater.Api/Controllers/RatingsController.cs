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
    public class RatingsController : CommonController<Rating>
    {
        public RatingsController(beeaterContext context)
            : base(context)
        {

        }

        [HttpGet("movie/{movieId}")]
        public async Task<ActionResult<IEnumerable<Rating>>> GetAllRatingsForMovie(int movieId)
        {
            var ratings = await _context.Ratings.Where(x => x.MovieId == movieId).ToListAsync();

            return ratings;
        }

        [HttpGet("user/{userId}/movie/{movieId}")]
        public async Task<ActionResult<Rating>> GetRatingByUserAndMovieId(string userId, int movieId)
        {
            var rating = await _context.Ratings.FirstOrDefaultAsync(x => x.UserId == userId && x.MovieId == movieId);

            if (rating != null)
                return Ok(rating);

            else
                return NotFound();
                 
        }
    }
}
