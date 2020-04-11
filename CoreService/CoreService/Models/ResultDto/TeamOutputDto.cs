using System.Collections.Generic;
using CoreService.Models.BaseDto;

namespace CoreService.Models.ResultDto
{
    public class TeamOutputDto:TeamBaseDto
    {
        public List<UserBaseDto> Members { get; set; }
        public List<AssetDto> Assets { get; set; }
    }
}