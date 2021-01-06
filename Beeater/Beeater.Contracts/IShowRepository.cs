using Beeater.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Beeater.Contracts
{
    public interface IShowRepository : IRepository<Show>
    {
        Task<IEnumerable<Show>> GetShowsByMovieId(int movieId);
        Task<IEnumerable<Show>> GetShowsByMovieTitle(string title);
        Task<IEnumerable<Show>> GetShowsByTheaterId(int theaterId);
        Task<object> GetShowWithSeatsAndSeatStatus(int showId);
    }
}
