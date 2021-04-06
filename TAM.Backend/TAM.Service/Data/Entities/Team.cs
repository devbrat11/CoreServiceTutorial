using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TAMService.Data.Entities
{
    public class Team
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TeamId { get; set; }
        [Key]
        public string Name { get; set; }
        public string Department { get; set; }
        public string ParentTeam { get; set; }
        public string Manager { get; set; }
        public string Lead { get; set; }
    }
}