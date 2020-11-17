using Beeater.Domain.Entities;
using Beeater.Persistence;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beeater.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private beeaterContext _context;
        public MoviesController(beeaterContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovieById(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie != null)
                return Ok(movie);

            else
                return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> PostMovies([FromBody]IEnumerable<Movie> movies)
        {
            await _context.Movies.AddRangeAsync(movies);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
