using CoreService.Models.BaseDto;

namespace CoreService.Models.ResultDto
{
    public class AssetOutputDto:AssetDto
    {
        public UserBaseDto Owner { get; set; }
    }
}