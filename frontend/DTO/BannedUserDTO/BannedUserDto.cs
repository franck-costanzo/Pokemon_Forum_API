using System;
using System.ComponentModel.DataAnnotations;

namespace Smogon_MAUIapp.DTO.BannedUserDTO
{
    public class BannedUserDto
    {
        /*@user_id, @banned_by_user_id, @ban_start_date, @ban_end_date, @reason*/
        [Required]
        public int user_id { get; set; }

        [Required]
        public int banned_by_user_id { get; set; }

        public DateTime ban_start_date { get; set; }

        [Required]
        public DateTime ban_end_date { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 20, ErrorMessage = "reason length must be between 20 and 255")]
        public string reason { get; set; }
    }
}
