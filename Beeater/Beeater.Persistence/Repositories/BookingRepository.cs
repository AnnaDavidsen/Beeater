using Beeater.Contracts;
using Beeater.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beeater.Persistence.Repositories
{
    public class BookingRepository : RepositoryBase<Booking>, IBookingRepository
    {
        public BookingRepository(beeaterContext context)
            : base(context)
        {
                
        }
    }
}
