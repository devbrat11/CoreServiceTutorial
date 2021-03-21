using TAMService.Models.BaseDto;

namespace TAMService.Models.ResultDto
{
    public class AssetOutputDto:AssetDto
    {
        public UserResultDto Owner { get; set; }
    }
}