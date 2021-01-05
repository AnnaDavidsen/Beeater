﻿using Beeater.Contracts;
using Beeater.Domain.Entities;
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
    public class GenresController : ControllerBase
    {
        private IRepositoryWrapper _repo;
        public GenresController(IRepositoryWrapper repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> GetAll()
        {
            var entities = await _repo.Genres.FindAll().ToListAsync();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> GetById(int id)
        {
            var entity = await _repo.Genres.FindByCondition(x => x.Id == id).FirstOrDefaultAsync();

            if (entity != null)
                return Ok(entity);

            else
                return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] IEnumerable<Genre> entities)
        {
            _repo.Genres.Create(entities);
            await _repo.SaveAsync();

            return Ok(entities);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] string value)
        {
            return Ok();
        }

    }
}
