using CoreService.Data.Repository;
using CoreService.Helpers;
using CoreService.Models.BaseDto;
using Microsoft.AspNetCore.Mvc;

namespace CoreService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly IDataStore _dataStore;

        public TeamsController(IDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        [HttpGet]
        public IActionResult GetTeams()
        {
            var teams = _dataStore.GetAllTeamsInformation();
            if (teams != null&&teams.Count>0)
            {
                return Ok(teams);
            }
            return NoContent();
        }

        [HttpGet("{teamName}")]
        public IActionResult GetTeams(string teamName)
        {
            var team = _dataStore.GetTeamInformation(teamName);
            if (team != null)
            {
                return Ok(team);
            }
            return NoContent();
        }

        [HttpGet("{teamName}/assets")]
        public IActionResult GetTeamAssetsInformation(string teamName)
        {
            var team = _dataStore.GetTeamAssets(teamName);
            if (team != null)
            {
                return Ok(team);
            }
            return NoContent();
        }

        [HttpGet("{teamName}/users")]
        public IActionResult GetTeamMembers(string teamName)
        {
            var team = _dataStore.GetTeamMembers(teamName);
            if (team != null)
            {
                return Ok(team);
            }
            return NoContent();
        }

        [HttpPost]
        public IActionResult AddTeam([FromBody]TeamBaseDto team)
        {
            var isTeamAdded = _dataStore.TryAddingTeam(team.ToEntity());
            if (isTeamAdded)
            {
                _dataStore.SaveChanges();
                return Ok($"Team {team.Name} added successfully.");
            }

            return BadRequest();
        }
    }
}