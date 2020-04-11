using CoreService.Data.Repository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using CoreService.Helpers;
using CoreService.Models.InputDto;

namespace CoreService.Controllers
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
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(Guid id)
        {
            var user = _dataStore.GetUser(id);
            if (user == null)
            {
                return NotFound($"User not found.");
            }
            return Ok(user);
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
        public IActionResult ValidateUser([FromBody]UserCredentialDto userCredential)
        {
            var userValidationInfo = _dataStore.IsUserValid(userCredential.EmailId, userCredential.Password);
            if (userValidationInfo.Item1)
            {
                return Ok(userValidationInfo.Item2);
            }

            return BadRequest("Invalid Credentials !");
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] UserRegistrationDto userRegistration)
        {
            var userEntity = userRegistration.GetUserEntities();
            var isUserRegistered = _dataStore.TryRegisteringUser(userEntity);
            if (isUserRegistered)
            {
                _dataStore.SaveChanges();
                return StatusCode(201, "User registered successfully.");
            }

            return BadRequest();
        }

        [HttpPut]
        public IActionResult UpdateUser(Guid userId, [FromBody] UserRegistrationDto userRegistration)
        {
            return Ok();
        }

        [HttpPatch]
        public IActionResult UpdateUserPartially(Guid userId, [FromBody] JsonPatchDocument<UserRegistrationDto> user)
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