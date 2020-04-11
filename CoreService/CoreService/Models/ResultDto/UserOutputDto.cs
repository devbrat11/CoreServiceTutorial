using System.Collections.Generic;
using CoreService.Models.BaseDto;

namespace CoreService.Models.ResultDto
{
    public class UserOutputDto:UserBaseDto
    {
        public TeamBaseDto Team { get; set; }
        public List<AssetDto> Assets { get; set; }
    }
}