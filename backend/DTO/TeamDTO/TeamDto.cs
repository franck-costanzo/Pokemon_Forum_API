using System;
using System.ComponentModel.DataAnnotations;

namespace Pokemon_Forum_API.DTO.TeamDTO
{
    public class TeamDto
    {
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "team name length must be between 3 and 20")]
        public string name { get; set; }

        [Required]
        public string link { get; set; }

        [Required]
        public int user_id { get; set; }
    }
}
