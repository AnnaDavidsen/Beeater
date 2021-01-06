using Beeater.Contracts;
using Beeater.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Beeater.Persistence.Repositories
{
    public class RatingRepository : RepositoryBase<Rating>, IRatingRepository
    {
        public RatingRepository(beeaterContext context)
            : base(context)
        {

        }

        public async Task<IEnumerable<Rating>> GetAllRatingsForMovie(int movieId)
        {
            var ratings = await FindByCondition(x => x.MovieId == movieId).ToListAsync();

            return ratings;
        }

        public async Task<Rating> GetRatingByUserAndMovieId(string userId, int movieId)
        {
            var rating = await FindByCondition(x => x.UserId == userId && x.MovieId == movieId)
                .FirstOrDefaultAsync();

            return rating;
        }
    }
}
