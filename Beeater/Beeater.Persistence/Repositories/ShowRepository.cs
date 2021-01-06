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
    public class ShowRepository : RepositoryBase<Show>, IShowRepository
    {
        public ShowRepository(beeaterContext context)
            : base(context)
        {

        }

        public async Task<IEnumerable<Show>> GetShowsByMovieId(int movieId)
        {
            var shows = await FindByCondition(x => x.MovieId == movieId)
                .Include(x => x.Theater)
                .ToListAsync();

            return shows;
        }

        public async Task<IEnumerable<Show>> GetShowsByMovieTitle(string title)
        {
            var shows = await FindByCondition(s => Context.Movies
                .Any(m => title == m.Title))
                .ToListAsync();

            return shows;
        }

        public async Task<IEnumerable<Show>> GetShowsByTheaterId(int theaterId)
        {
            var shows = await FindByCondition(s => s.TheaterId == theaterId)
                .ToListAsync();

            return shows;
        }

        public async Task<object> GetShowWithSeatsAndSeatStatus(int showId)
        {
            var show = await FindByCondition(x => x.Id == showId)
                .Include(x => x.Theater)
                    .ThenInclude(x => x.Seats)
                .FirstOrDefaultAsync();

            var bookings = await Context.Bookings.Where(x => x.ShowId == showId)
                .ToListAsync();

            if (show != null)
            {
                return new
                {
                    id = show.Id,
                    seats = show.Theater.Seats.Select(x => new { x.Id, x.Row, x.Number }),
                    occupiedSeats = bookings.Select(x => x.SeatId)
                };
            }
            else
            {
                return null;
            }
        }
    }
}
