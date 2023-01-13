using System.ComponentModel.DataAnnotations;

namespace Pokemon_Forum_API.DTO.LikeDTO
{
    public class LikeDto
    {
        [Required]
        public int post_id { get; set; }

        [Required]
        public int user_id { get; set; }
    }
}
