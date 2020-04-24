using System.Collections.Generic;
using System.Linq;
using CoreService.Data.Repository;
using CoreService.Helpers;
using CoreService.Models.BaseDto;
using CoreService.Models.ResultDto;
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

        [HttpPost]
        public IActionResult AddTeam([FromBody]TeamDto team)
        {
            var isTeamAdded = _dataStore.TryAddingTeam(team.ToEntity());
            if (isTeamAdded)
            {
                _dataStore.SaveChanges();
                return Ok($"Team {team.Name} added successfully.");
            }

            return BadRequest();
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
            var result = new List<AssetOutputDto>();
            var teamAssets = _dataStore.GetTeamAssets(teamName);
            if (teamAssets == null||!teamAssets.Any())
            {
                return NoContent();
            }

            foreach (var teamAsset in teamAssets)
            {
                var user = _dataStore.GetUser(teamAsset.OwnerId);
                result.Add(teamAsset.AsAssetOutputDto(user.AsUserResultDto()));
            }
            return Ok(result);
        }

        [HttpGet("{teamName}/users")]
        public IActionResult GetTeamMembers(string teamName)
        {
            var result = new List<UserResultDto>();
            var teamMembers = _dataStore.GetTeamMembers(teamName);
            if (teamMembers == null||!teamMembers.Any())
            {
                return NoContent();
            }

            foreach (var teamMember in teamMembers)
            {
                var assets = _dataStore.GetUserAssets(teamMember.Id);
                result.Add(teamMember.AsUserResultDto(null, assets));
            }
            return Ok(result);
        }
    }
}