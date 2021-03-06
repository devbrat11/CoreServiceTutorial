﻿using System;
using System.ComponentModel.DataAnnotations;

namespace TAM.Service.Models
{
    public class TeamDto
    {
        public Guid ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Department { get; set; }
        public string ParentTeam { get; set; }
        public string Manager { get; set; }
        public string Lead { get; set; }
    }
}