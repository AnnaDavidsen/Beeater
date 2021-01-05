using Beeater.Contracts;
using Beeater.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Beeater.Persistence.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private beeaterContext _context;
        private IBookingRepository _bookings;
        private IGenreRepository _genres;
        private IMovieRepository _movies;
        private IRatingRepository _ratings;
        private ITheaterRepository _theaters;
        private IShowRepository _shows;
        private IUserRepository _users;

        public RepositoryWrapper(beeaterContext context)
        {
            _context = context;
        }

        public IBookingRepository Bookings
        {
            get
            {
                if (_bookings == null) _bookings = new BookingRepository(_context);
                return _bookings;
            }
        }

        public IGenreRepository Genres
        {
            get
            {
                if (_genres == null) _genres = new GenreRepository(_context);
                return _genres;
            }
        }

        public IMovieRepository Movies
        {
            get
            {
                if (_movies == null) _movies = new MovieRepository(_context);
                return _movies;
            }
        }

        public IRatingRepository Ratings
        {
            get
            {
                if (_ratings == null) _ratings = new RatingRepository(_context);
                return _ratings;
            }
        }

        public ITheaterRepository Theaters
        {
            get
            {
                if (_theaters == null) _theaters = new TheaterRepository(_context);
                return _theaters;
            }
        }

        public IShowRepository Shows
        {
            get
            {
                if (_shows == null) _shows = new ShowRepository(_context);
                return _shows;
            }
        }

        public IUserRepository Users
        {
            get
            {
                if (_users == null) _users = new UserRepository(_context);
                return _users;
            }
        }


        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
