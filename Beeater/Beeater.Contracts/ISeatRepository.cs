using Beeater.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Beeater.Contracts
{
    public interface ISeatRepository : IRepository<Seat>
    {
        void CreateSeats(int[] rows, int theaterId);
    }
}
