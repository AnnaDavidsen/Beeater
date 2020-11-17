using Beeater.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beeater.Api.Controllers
{
    [Route("api/[controller]")]
    public class CommonController<T> : ControllerBase where T : class
    {
        protected beeaterContext _context;
        public CommonController(beeaterContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<T>>> GetAll()
        {
            var entities = await _context.Set<T>().ToListAsync();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<T>> GetById(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);

            if (entity != null)
                return Ok(entity);

            else
                return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
