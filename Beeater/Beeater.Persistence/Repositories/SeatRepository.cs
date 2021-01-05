using Beeater.Contracts;
using Beeater.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Beeater.Persistence.Repositories
{
    public class SeatRepository : RepositoryBase<Seat>, ISeatRepository
    {
        public SeatRepository(beeaterContext context)
            : base(context)
        {

        }
    }
}
