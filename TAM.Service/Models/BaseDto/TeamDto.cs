using System.ComponentModel.DataAnnotations;

namespace TAMService.Models.BaseDto
{
    public class TeamDto
    {
        [Required]
        public string Name { get; set; }
        public string Department { get; set; }
        public string ParentTeam { get; set; }
        public string Manager { get; set; }
        public string Lead { get; set; }
    }
}