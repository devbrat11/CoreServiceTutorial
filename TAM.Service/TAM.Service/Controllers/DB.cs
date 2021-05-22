using Microsoft.AspNetCore.Mvc;
using TAMService.Data.DataStore;

namespace TAM.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DB : ControllerBase
    {
        private readonly TAMServiceContext _dbContext;

        public DB(TAMServiceContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("clear-data")]
        public IActionResult ValidateUser()
        {
            _dbContext.Users.Clear();
            _dbContext.UserCredentials.Clear();
            _dbContext.Teams.Clear();
            _dbContext.Assets.Clear();
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
