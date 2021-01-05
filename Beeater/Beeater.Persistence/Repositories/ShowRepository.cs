using Beeater.Contracts;
using Beeater.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Beeater.Persistence.Repositories
{
    public class ShowRepository : RepositoryBase<Show>, IShowRepository
    {
        public ShowRepository(beeaterContext context)
            : base(context)
        {

        }
    }
}
