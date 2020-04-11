using System.ComponentModel.DataAnnotations;

namespace CoreService.Data.Entities
{
    public class Team
    {
        [Key]
        public string Name { get; set; }
        public string Department { get; set; }
        public string ParentTeam { get; set; }
        public string Manager { get; set; }
        public string Lead { get; set; }
    }
}