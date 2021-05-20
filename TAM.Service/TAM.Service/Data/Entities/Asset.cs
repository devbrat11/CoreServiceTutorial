using System;
using System.ComponentModel.DataAnnotations;
using TAM.Service.Models.Enum;

namespace TAMService.Data.Entities
{
    public class Asset
    {
        [Key]
        public string SerialNumber { get; set; }
        public string Brand { get; set; }
        public Guid OwnerId { get; set; }
        public Guid TeamId { get; set; }
        public string HostName { get; set; }
        public AssetType Type { get; set; }
    }
}