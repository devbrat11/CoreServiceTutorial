namespace CoreService.Models.BaseDto
{
    public class AssetDto
    {
        public virtual string Type { get; set; }
        public string ModelNumber { get; set; }
        public virtual string SerialNumber { get; set; }
        public string Brand { get; set; }
        public string HostName { get; set; }
    }
}