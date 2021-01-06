using Beeater.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Beeater.Contracts
{
    public interface ITheaterRepository : IRepository<Theater>
    {
        Task<IEnumerable<Theater>> GetAllTheatersWithSeats();
        void CreateTheater(Theater theater);

    }
}
