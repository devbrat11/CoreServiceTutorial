using System;
using System.ComponentModel.DataAnnotations;

namespace TAMService.Data.Entities
{
    public class Asset
    {
        public string Type { get; set; }
        public string ModelNumber { get; set; }
        [Key]
        public string SerialNumber { get; set; }
        public string Brand { get; set; }
        public Guid OwnerId { get; set; }
        public string HostName { get; set; }
    }
}