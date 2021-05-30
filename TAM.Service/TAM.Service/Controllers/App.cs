using Microsoft.AspNetCore.Mvc;
using System;
using TAM.Service.Models.Enum;
using TAM.Service.Data.DataStore;
using TAM.Service.Data.Entities;
using TAM.Service.Data.Repository;
using TAM.Service.Models;
using TAM.Service.Helpers;

namespace TAM.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class App : ControllerBase
    {
        private readonly TAMServiceContext _dbContext;
        private readonly IDataStore _dataStore;

        public App(TAMServiceContext dbContext, IDataStore dataStore)
        {
            _dbContext = dbContext;
            _dataStore = dataStore;
        }

        [HttpGet]
        public IActionResult AppState()
        {
            return Ok("App is running...");
        }

        [HttpPost("seed-db")]
        public IActionResult Seed()
        {
            _dataStore.TryAddingTeam(new Team() 
            {
                Name = "NightWatch",
                Department = "Security",
                ParentTeam = "GOT",
                Manager = "Jon Snow",
                Lead = "Jon Snow"
            });

            _dataStore.TryRegisteringAsset(new Asset()
            {
                TeamId = Guid.Empty,
                HostName = "HST1234",
                SerialNumber = "S1234",
                Brand = "Apple",
                Type = AssetType.Laptop,
                OwnerId = Guid.Empty

            });

            var registrationInfo = new UserRegistrationInfo()
            {
                Name = "Devbrat",
                DateOfBirth = new DateTime(1993, 07, 11),
                EmailId = "test@TAM.com",
                Password = "test1234",
                Team = "NightWatch",
            }.ToEntity();

            _dataStore.TryRegisteringUser(registrationInfo.Item1, registrationInfo.Item2);

            _dataStore.SaveChanges();

            return Ok();
        }

        [HttpDelete("clear-db")]
        public IActionResult ClearData()
        {
            _dbContext.Users.Clear();
            _dbContext.UserCredentials.Clear();
            _dbContext.Teams.Clear();
            _dbContext.Assets.Clear();
            _dbContext.Sessions.Clear();
            _dbContext.SaveChanges();
            return Ok();
        }


    }
}
