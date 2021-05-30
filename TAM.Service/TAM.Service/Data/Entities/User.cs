using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TAM.Service.Data.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PK { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Team { get; set; }
        public string EmailId { get; set; }
    }
}
