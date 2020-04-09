namespace CoreService.Models
{
    public class AssetDto
    {
        public string Type { get; set; }
        public string ModelNumber { get; set; }
        public string SerialNumber { get; set; }
        public string Brand { get; set; }
        public string AllocatedToUserId { get; set; }
    }
}