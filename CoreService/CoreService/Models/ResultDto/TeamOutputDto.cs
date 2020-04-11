using System.Collections.Generic;
using CoreService.Models.BaseDto;

namespace CoreService.Models.ResultDto
{
    public class TeamOutputDto:TeamDto
    {
        public List<UserResultDto> Members { get; set; }
        public List<AssetOutputDto> Assets { get; set; }
    }
}