using System.Collections.Generic;
using TAMService.Models.BaseDto;

namespace TAMService.Models.ResultDto
{
    public class TeamOutputDto:TeamDto
    {
        public List<UserResultDto> Members { get; set; }
        public List<AssetOutputDto> Assets { get; set; }
    }
}