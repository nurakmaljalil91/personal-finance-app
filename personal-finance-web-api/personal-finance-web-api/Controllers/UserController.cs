using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using personal_finance_web_api.Models;
using personal_finance_web_api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace personal_finance_web_api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UsersRepository _usersRepository;

        public UserController(UsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        [HttpGet]
        public async Task<ActionResult<User>> ReadUsers()
        {
            var users = await _usersRepository.ReadUsers();
            return Ok(users);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<User>> ReadUser(string userId)
        {
            var user = await _usersRepository.ReadUser(userId);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Message>> CreateUser([FromBody] User user)
        {
            var created = await _usersRepository.CreateUser(user);

            return Ok(created);
        }

        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Message>> UpdateUser([FromBody] User user)
        {
            var updated = await _usersRepository.UpdateUser(user);

            return Ok(updated);
        }

        [HttpDelete("{userId}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Message>> DeleteUser(string userId)
        {
            var deleted = await _usersRepository.DeleteUser(userId);

            return Ok(deleted);
        }

    }
}
