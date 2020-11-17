using Beeater.Domain.Entities;
using Beeater.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beeater.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : CommonController<Genre>
    {
        public GenresController(beeaterContext context)
            : base (context)
        {
        }

        // GetAll, GetById and Post are located in CommonController class

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

    }
}
