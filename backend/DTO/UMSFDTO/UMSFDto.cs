using System.ComponentModel.DataAnnotations;

namespace Pokemon_Forum_API.DTO.UMSFDTO
{
    public class UMSFDto
    {
        [Required]
        public int user_id { get; set; }

        [Required]
        public int subforum_id { get; set; }
    }
}
