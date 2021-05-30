using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TAM.Service.Data.Entities
{
    public class Team
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string ParentTeam { get; set; }
        public string Manager { get; set; }
        public string Lead { get; set; }
    }
}