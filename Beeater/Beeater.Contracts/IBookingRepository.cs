using Beeater.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beeater.Contracts
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<Booking> GetBookingDetailed(int id);
        Task<IEnumerable<Booking>> GetBookingsForUser(string userId);
    }
}