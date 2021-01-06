using Beeater.Contracts;
using Beeater.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Beeater.Persistence.Repositories
{
    public class TheaterRepository : RepositoryBase<Theater>, ITheaterRepository
    {
        public TheaterRepository(beeaterContext context)
            : base(context)
        {

        }

        public void CreateTheater(Theater theater)
        {
            Create(new Theater[] { theater });
        }

        public async Task<IEnumerable<Theater>> GetAllTheatersWithSeats()
        {
            var theaters = await FindAll().Include(x => x.Seats).ToListAsync();
            return theaters;
        }
    }
}
