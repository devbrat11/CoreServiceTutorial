using System.Collections.Generic;

namespace TAMService.Models.ResultDto
{
    public class TeamOutputDto:TeamDto
    {
        public List<UserResultDto> Members { get; set; }
        public List<AssetDto> Assets { get; set; }
    }
}