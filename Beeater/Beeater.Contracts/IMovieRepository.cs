using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Beeater.Domain.Entities;

namespace Beeater.Contracts
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<IEnumerable<object>> GetMoviesWithGenre();
        Task<IEnumerable<object>> GetMoviesWithGenreByGenreAndTitle(string genre, string title);
        Task<Movie> GetMovieByTitle(string title);
        Task<Movie> GetMovieDetailed(int id);
        Task<IEnumerable<Movie>> GetMoviesByGenreId(int genreId);
        Task<IEnumerable<object>> GetMoviesWithUpcomingShows();
    }
}
