using Beeater.Contracts;
using Beeater.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Beeater.Persistence.Repositories
{
    public class RatingRepository : RepositoryBase<Rating>, IRatingRepository
    {
        public RatingRepository(beeaterContext context)
            : base(context)
        {

        }
    }
}
