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
    public class BookingRepository : RepositoryBase<Booking>, IBookingRepository
    {
        public BookingRepository(beeaterContext context)
            : base(context)
        {
                
        }

        public async Task<Booking> GetBookingDetailed(int id)
        {
            var booking = await FindByCondition(x => x.Id == id)
                .Include(x => x.Seat)
                .Include(x => x.Show)
                .FirstOrDefaultAsync();

            return booking;
        }

        public async Task<IEnumerable<Booking>> GetBookingsForUser(string userId)
        {
            var bookings = await FindByCondition(x => x.UserId == userId)
                .Include(x => x.Show)
                    .ThenInclude(x => x.Movie)
                .Include(x => x.Show)
                    .ThenInclude(x => x.Theater)
                .Include(x => x.Seat)
                .ToListAsync();

            return bookings;
        }
    }
}
