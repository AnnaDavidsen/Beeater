﻿using Beeater.Contracts;
using Beeater.Domain.Entities;
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
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IRepositoryWrapper _repo;
        public UsersController(IRepositoryWrapper repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetAll()
        {
            var entities = await _repo.Users.FindAll().ToListAsync();
            return Ok(entities);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] IEnumerable<User> entities)
        {
            _repo.Users.Create(entities);
            await _repo.SaveAsync();

            return Ok(entities);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] IEnumerable<User> entities)
        {
            _repo.Users.Update(entities);
            await _repo.SaveAsync();
            return Ok(entities);

        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<User>> GetUserById(string id)
        {
            var user = await _repo.Users
                .FindByCondition(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (user != null)
                return Ok(user);
            else
                return NotFound();
        }

        [HttpGet("{firstName}/{lastName}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersByFullName(string firstName, string lastName)
        {
            var users = await _repo.Users.GetUsersByFullName(firstName, lastName);

            return Ok(users);
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<User>> GetUserByEmail(string email)
        {
            var user = await _repo.Users.GetUserByEmail(email);

            return Ok(user);
        }

        [HttpGet("points/{minPoints}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersWithPointsGreaterThan(int minpoints)
        {
            var users = await _repo.Users.FindByCondition(x => x.BonusPoints > minpoints).ToListAsync();

            return Ok(users);
        }


        [HttpDelete("stringid/{id}")]
        public async Task<ActionResult> DeleteByStringId(string id)
        {
            await _repo.Users.DeleteUser(id);

            await _repo.SaveAsync();

            return Ok();
        }
    }
}
