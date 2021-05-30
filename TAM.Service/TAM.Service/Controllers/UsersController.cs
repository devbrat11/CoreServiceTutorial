using TAM.Service.Data.Repository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TAM.Service.Helpers;
using TAM.Service.Models.ResultDto;
using TAM.Service.Models;
using TAM.Service.Data.Entities;
using Microsoft.Extensions.Primitives;

namespace TAM.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDataStore _dataStore;

        public UsersController(IDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _dataStore.GetUsers();
            var resultUsers = new List<UserDetails>();
            foreach (var user in users)
            {
                var team = _dataStore.GetTeam(user.Team);
                var assets = _dataStore.GetUserAssets(user.PK);
                resultUsers.Add(user.ToDto(team, assets));
            }
            if (!resultUsers.Any())
            {
                return NotFound();
            }
            return Ok(resultUsers);
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] UserRegistrationInfo userRegistrationInfo)
        {
            var registrationInfo = userRegistrationInfo.ToEntity();
            var isUserRegistered = _dataStore.TryRegisteringUser(registrationInfo.Item1, registrationInfo.Item2);
            if (isUserRegistered)
            {
                _dataStore.SaveChanges();
                return StatusCode(201);
            }

            return BadRequest();
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(Guid id)
        {
            var user = _dataStore.GetUser(id);
            if (user == null)
            {
                return NotFound($"User not found.");
            }
            var team = _dataStore.GetTeam(user.Team);
            var assets = _dataStore.GetUserAssets(user.PK);
            var resultUser = user.ToDto(team, assets);
            return Ok(resultUser);
        }

        [HttpGet("{id}/assets")]
        public IActionResult GetUserAssets(Guid id)
        {
            var assets = _dataStore.GetUserAssets(id);
            if (assets == null)
            {
                return NotFound($"No Asset.");
            }
            return Ok(assets);
        }

        [HttpPost("authenticate")]
        public IActionResult ValidateUser([FromBody] LoginDetails loginDetails)
        {
            var userValidationInfo = _dataStore.IsUserValid(loginDetails);
            if (userValidationInfo.Item1)
            {
                return Ok(userValidationInfo.Item2);
            }

            return NotFound("Invalid Credentials !");
        }

        [HttpPut]
        public IActionResult UpdateUser(Guid userId, [FromBody] UserRegistrationInfo userRegistration)
        {
            return Ok();
        }

        [HttpPatch]
        public IActionResult UpdateUserPartially(Guid userId, [FromBody] JsonPatchDocument<UserRegistrationInfo> user)
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