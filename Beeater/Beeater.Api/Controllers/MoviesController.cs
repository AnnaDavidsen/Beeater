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

        [HttpGet("withgenre")]
        public async Task<ActionResult<IEnumerable<object>>> GetMovieByTitle()
        {
            //Besked til Anna:
            //Det virker fint.... Men det må kunne gøres kønnere?
            var movies = await _context.Movies
                .Join(_context.Genres,
                    m => m.GenreId,
                    g => g.Id,
                    (m, g) => new { movie = m, genre = g }
                    ).ToListAsync();
            return movies;
        }



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

        [HttpGet("genre/{genreId}")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMoviesByGenreId(int genreId)
        {
            var movies = await _context.Movies.Where(x => x.GenreId == genreId).ToListAsync();

            return Ok(movies);
        }
    }
}
