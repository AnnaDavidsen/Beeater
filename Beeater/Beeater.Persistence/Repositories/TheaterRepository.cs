using Beeater.Contracts;
using Beeater.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Beeater.Persistence.Repositories
{
    public class TheaterRepository : RepositoryBase<Theater>, ITheaterRepository
    {
        public TheaterRepository(beeaterContext context)
            : base(context)
        {

        }
    }
}
