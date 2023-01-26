using Pokemon_Forum_API.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Pokemon_Forum_API.DTO.ThreadDTO
{
    public class ThreadDto
    {
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "title length must be between 3 and 20")]
        public string title { get; set; }

        [Required]
        public DateTime create_date { get; set; }


        public DateTime? last_post_date { get; set; }

        [Required]
        public int user_id { get; set; }

        [Required]
        public int subforum_id { get; set; }
    }
}
