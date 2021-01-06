using Beeater.Contracts;
using Beeater.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beeater.Persistence.Repositories
{
    public class MovieRepository : RepositoryBase<Movie>, IMovieRepository
    {
        public MovieRepository(beeaterContext context)
            : base(context)
        {

        }

        public async Task<Movie> GetMovieByTitle(string title)
        {
            var movie = await FindByCondition(x => x.Title.ToLower() == title.ToLower())
                .FirstOrDefaultAsync();

            return movie;
        }

        public async Task<Movie> GetMovieDetailed(int id)
        {
            var movie = await FindByCondition(x => x.Id == id)
                .Include(x => x.Trailers)
                .Include(x => x.Ratings)
                .FirstOrDefaultAsync();
            
            return movie;
        }

        public async Task<IEnumerable<Movie>> GetMoviesByGenreId(int genreId)
        {
            return await FindByCondition(x => x.GenreId == genreId).ToListAsync();
        }

        public async Task<IEnumerable<object>> GetMoviesWithGenre()
        {
            var movies = FindAll();

            var a = await movies.Join(Context.Genres,
                m => m.GenreId,
                g => g.Id,
                (m, g) => new { movie = m, genre = g })
                .ToListAsync();

            return a;
        }

        public async Task<IEnumerable<object>> GetMoviesWithGenreByGenreAndTitle(string genre, string title)
        {
            var movies = FindAll()
                .Include(x => x.Shows)
                .Where(x => x.Shows.Count > 0);

            var a = await movies.Join(Context.Genres,
                m => m.GenreId,
                g => g.Id,
                (m, g) => new { movie = m, genre = g }).Where(mg =>
                    (mg.movie.Title.ToLower().Contains(title.ToLower())
                    || title.ToLower() == "_all_")
                    && (mg.genre.Name.ToLower() == genre.ToLower()
                    || genre.ToLower() == "_all_")
                    )
                .ToListAsync();

            var upcoming = new List<object>();

            foreach (var item in a)
            {
                if (item.movie.Shows.Any(x => x.ShowTime > DateTime.Now))
                    upcoming.Add(item);
            }

            return upcoming;
        }

        public async Task<IEnumerable<object>> GetMoviesWithUpcomingShows()
        {
            var movies = await FindAll()
                .Include(x => x.Shows)
                .Where(x => x.Shows.Count > 0)
                .Join(Context.Genres,
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

            return upcoming;
        }
    }
}
