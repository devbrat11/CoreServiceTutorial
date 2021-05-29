using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TAM.Service.Data.Entities
{
    public class UserCredential
    {
        [Key]
        public string UserID { get; set; }
        public string Password { get; set; }
    }

    public class Session
    {
        [Key]
        public string UserID { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid SessionID { get; set; }
    }
}
