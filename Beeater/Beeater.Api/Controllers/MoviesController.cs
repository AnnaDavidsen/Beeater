using Beeater.Contracts;
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
    public class MoviesController : ControllerBase
    {
        private IRepositoryWrapper _repo;
        public MoviesController(IRepositoryWrapper repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetAll()
        {
            var entities = await _repo.Movies.FindAll().ToListAsync();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetById(int id)
        {
            var entity = await _repo.Movies.FindByCondition(x => x.Id == id).FirstOrDefaultAsync();

            if (entity != null)
                return Ok(entity);

            else
                return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] IEnumerable<Movie> entities)
        {
            _repo.Movies.Create(entities);
            await _repo.SaveAsync();

            return Ok(entities);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] IEnumerable<Movie> entities)
        {
            _repo.Movies.Update(entities);
            await _repo.SaveAsync();
            return Ok(entities);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var toDelete = await _repo.Movies.FindByCondition(x => x.Id == id).ToListAsync();
            _repo.Movies.Delete(toDelete);

            await _repo.SaveAsync();

            return Ok(toDelete);
        }

        [HttpGet("withgenre")]
        public async Task<ActionResult<IEnumerable<object>>> GetMoviesWithGenre()
        {
            var movies =  _repo.Movies.FindAll();

            var a = await movies.Join(_repo.Genres.FindAll(),
                m => m.GenreId,
                g => g.Id,
                (m, g) => new { movie = m, genre = g })
                .ToListAsync();

            return a;
        }

        [HttpGet("withgenre/genretitle/{genre}/{title}")]
        public async Task<ActionResult<IEnumerable<object>>> GetMoviesWithGenreByGenreAndTitle(string genre, string title)
        {
            var movies = _repo.Movies.FindAll();

            var a = await movies.Join(_repo.Genres.FindAll(),
                    m => m.GenreId,
                    g => g.Id,
                    (m, g) => new { movie = m, genre = g }
                    ).Where(mg =>
                    (mg.movie.Title.ToLower().Contains(title.ToLower())
                    || title.ToLower() == "_all_")
                    && (mg.genre.Name.ToLower() == genre.ToLower()
                    || genre.ToLower() == "_all_")
                )
                .ToListAsync();

            return a;
        }

        [HttpGet("title/{title}")]
        public async Task<ActionResult<Movie>> GetMovieByTitle(string title)
        {
            var movie = await _repo.Movies
                .FindByCondition(x => x.Title.ToLower() == title.ToLower())
                .FirstOrDefaultAsync();

            if (movie != null)
                return Ok(movie);

            else
                return NotFound();
        }

        [HttpGet("{id}/detailed")]
        public async Task<ActionResult<Movie>> GetMovieByIdDetailed(int id)
        {
            var movie = await _repo.Movies
                .Include(x => x.Trailers, x => x.Ratings)
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
            var movies = await _repo.Movies.FindByCondition(x => x.GenreId == genreId).ToListAsync();

            return Ok(movies);
        }

        [HttpGet("upcoming")]
        public async Task<ActionResult<IEnumerable<object>>> GetMoviesWithUpcomingShows()
        {
            var movies = await _repo.Movies
                .Include(x => x.Shows)
                .Where(x => x.Shows.Count > 0)
                .Join(_repo.Genres.FindAll(),
                    m => m.GenreId,
                    g => g.Id,
                    (m, g) => new { movie = m, genre = g })
                .ToListAsync();

            var upcoming = new List<object>();

            foreach (var item in movies)
            {
                if (item.movie.Shows.Any(x => x.ShowTime > DateTime.Now))
                    upcoming.Add(item);
            }

            return Ok(upcoming);
        }

    }
}
