﻿using System.ComponentModel.DataAnnotations;

namespace Smogon_MAUIapp.DTO.ForumDTO
{
    public class ForumDto
    {
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "forum name length must be between 3 and 20")]
        public string name { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 20, ErrorMessage = "description length must be between 20 and 255")]
        public string description { get; set; }
    }
}
