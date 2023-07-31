using System.ComponentModel.DataAnnotations;

namespace Smogon_MAUIapp.DTO.LikeDTO
{
    public class LikeDto
    {
        [Required]
        public int post_id { get; set; }

        [Required]
        public int user_id { get; set; }
    }
}
