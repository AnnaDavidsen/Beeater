using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Beeater.Contracts
{
    public interface IRepositoryWrapper
    {
        IBookingRepository Bookings { get; }
        IGenreRepository Genres { get; }
        IMovieRepository Movies { get; }
        IRatingRepository Ratings { get; }
        ITheaterRepository Theaters { get; }
        IShowRepository Shows { get; }
        IUserRepository Users { get; }
        ISeatRepository Seats { get; }
        Task SaveAsync();
    }
}
