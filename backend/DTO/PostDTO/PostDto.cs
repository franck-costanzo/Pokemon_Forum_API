using System;
using System.ComponentModel.DataAnnotations;

namespace Pokemon_Forum_API.DTO.PostDTO
{
    public class PostDto
    {
        [Required]
        [StringLength(60000, MinimumLength = 30, ErrorMessage = "description length must be between 30 and 60000")]
        public string content { get; set; }

        public DateTime create_date { get; set; }

        [Required]
        public int thread_id { get; set; }

        [Required]
        public int user_id { get; set; }
    }
}
