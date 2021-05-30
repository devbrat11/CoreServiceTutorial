using System.ComponentModel.DataAnnotations;

namespace TAM.Service.Data.Entities
{
    public class LoginDetails
    {
        [Key]
        public string UserID { get; set; }
        public string Password { get; set; }
    }

}
