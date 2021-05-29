using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TAMService.Data.Entities
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PK { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Team { get; set; }
        [Key]
        public string EmailId { get; set; }
    }
}
