using CoreService.Models.BaseDto;

namespace CoreService.Models.ResultDto
{
    public class AssetOutputDto:AssetDto
    {
        public UserResultDto Owner { get; set; }
    }
}