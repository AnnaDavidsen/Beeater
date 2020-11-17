using Beeater.Domain.Entities;
using Beeater.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beeater.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : CommonController<Booking>
    {
        public BookingsController(beeaterContext context)
            : base(context)
        {

        }
        // GetAll, GetById and Post are located in CommonController class

    }
}
