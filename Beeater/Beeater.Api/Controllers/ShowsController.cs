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
    public class ShowsController : CommonController<Show>
    {
        public ShowsController(beeaterContext context)
            : base(context)
        {

        }
        [HttpGet("movieid/{movieid}")]
        public async Task<ActionResult<IEnumerable<Show>>> GetShowByMovieTitle(int movieId)
        {
            var shows = await _context.Shows
                .Where(s => s.MovieId == movieId)
                .ToListAsync();

            return Ok(shows);
        }

        [HttpGet("title/{title}")]
        public async Task<ActionResult<IEnumerable<Show>>> GetShowByMovieTitle(string title)
        {
            var shows = await _context.Shows
                .Where(s => _context.Movies
                .Any(m => title == m.Title))
                .ToListAsync();

            return Ok(shows);
        }
        [HttpGet("theater/{theaterId}")]
        public async Task<ActionResult<IEnumerable<Show>>> GetShowsByTheaterId(int theaterId)
        {
            var shows = await _context.Shows
                .Where(s => s.TheaterId == theaterId)
                .ToListAsync();

            return Ok(shows);
        }
    }
}
