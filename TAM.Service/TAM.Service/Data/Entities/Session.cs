using System;
using System.ComponentModel.DataAnnotations;

namespace TAM.Service.Data.Entities
{
    public class Session
    {
        [Key]
        public string UserID { get; set; }
        public Guid SessionID { get; set; }
    }
}
