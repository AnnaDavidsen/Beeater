﻿using Beeater.Persistence;
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
            _context.Set<T>().AddRange(entities);
            await _context.SaveChangesAsync();

            return Ok(entities);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] IEnumerable<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
            await _context.SaveChangesAsync();
            return Ok(entities);

        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            await _context.SaveChangesAsync();
            return Ok(entities);
        } 

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var toDelete = await _context.Set<T>().FindAsync(id);
            _context.Set<T>().Remove(toDelete);

            await _context.SaveChangesAsync();

            return Ok(toDelete);
        }

    }
}
