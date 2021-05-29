using Microsoft.AspNetCore.Mvc;
using System;
using TAM.Service.Models.Enum;
using TAMService.Data.DataStore;
using TAMService.Data.Entities;
using TAMService.Data.Repository;
using TAMService.Helpers;
using TAMService.Models;

namespace TAM.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DB : ControllerBase
    {
        private readonly TAMServiceContext _dbContext;
        private readonly IDataStore _dataStore;

        public DB(TAMServiceContext dbContext, IDataStore dataStore)
        {
            _dbContext = dbContext;
            _dataStore = dataStore;
        }

        [HttpPost("clear")]
        public IActionResult ValidateUser()
        {
            _dbContext.Users.Clear();
            _dbContext.UserCredentials.Clear();
            _dbContext.Teams.Clear();
            _dbContext.Assets.Clear();
            _dbContext.Sessions.Clear();
            _dbContext.SaveChanges();
            return Ok();
        }


        [HttpPost("seed")]
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
            _dataStore.SaveChanges();

            _dataStore.TryRegisteringAsset(new Asset()
            {
                TeamId = Guid.Empty,
                HostName = "HST1234",
                SerialNumber = "S1234",
                Brand = "Apple",
                Type = AssetType.Laptop,
                OwnerId = Guid.Empty

            });

            _dataStore.SaveChanges();

            _dataStore.TryRegisteringUser(new UserRegistrationInfo()
            {
                Name = "Devbrat",
                DateOfBirth = new DateTime(1993, 07, 11),
                EmailId = "test@TAM.com",
                Password = "test1234",
                Team = "NightWatch",
            });

            _dataStore.SaveChanges();

            return Ok();
        }
    }
}
