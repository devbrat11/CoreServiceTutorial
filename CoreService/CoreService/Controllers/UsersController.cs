using System;
using CoreService.Data;
using CoreService.Data.Entities;
using CoreService.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CoreService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IDataStore _dataStore;

        public UsersController(IDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _dataStore.GetUsers();
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(string id)
        {
            var user  = _dataStore.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] UserDto user)
        {
            _dataStore.AddUser(new User()
            {
                Name = user.FirstName+" "+user.LastName,
                Age = user.Age
            });
            _dataStore.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateUser(Guid userId,[FromBody] UserDto user)
        {
            return Ok();
        }

        [HttpPatch]
        public IActionResult UpdateUserPartially(Guid userId,[FromBody] JsonPatchDocument<UserDto> user)
        {
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteUser(Guid userId)
        {
            return Ok();
        }
    }
}