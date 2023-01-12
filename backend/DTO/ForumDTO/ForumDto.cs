using Pokemon_Forum_API.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pokemon_Forum_API.DTO.ForumDTO
{
    public class ForumDto
    {
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "username length must be between 3 and 20")]
        public string name { get; set; }
        [Required]
        public string description { get; set; }
    }
}
