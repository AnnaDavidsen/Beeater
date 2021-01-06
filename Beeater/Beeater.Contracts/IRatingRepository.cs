using Beeater.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Beeater.Contracts
{
    public interface IRatingRepository : IRepository<Rating>
    {
        Task<IEnumerable<Rating>> GetAllRatingsForMovie(int movieId);
        Task<Rating> GetRatingByUserAndMovieId(string userId, int movieId);
    }
}
