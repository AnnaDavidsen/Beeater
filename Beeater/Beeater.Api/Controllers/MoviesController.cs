using Beeater.Domain.Entities;
using Beeater.Persistence;
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
    public class MoviesController : CommonController<Movie>
    {
        public MoviesController(beeaterContext context)
            : base(context)
        {
        }

        // GetAll, GetById and Post are located in CommonController class

        [HttpGet("title/{title}")]
        public async Task<ActionResult<Movie>> GetMovieByTitle(string title)
        {
            var movie = await _context.Movies
                .Where(x => x.Title.ToLower() == title.ToLower())
                .FirstOrDefaultAsync();

            if (movie != null)
                return Ok(movie);

            else
                return NotFound();
        }

        [HttpGet("{id}/detailed")]
        public async Task<ActionResult<Movie>> GetMovieByIdDetailed(int id)
        {
            var movie = await _context.Movies
                .Include(a => a.Trailers)
                .Include(a => a.Ratings)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (movie != null)
                return Ok(movie);

            else
                return NotFound();
        }
    }
}
