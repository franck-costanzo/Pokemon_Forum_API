using System.ComponentModel.DataAnnotations;

namespace Smogon_MAUIapp.DTO.TopicDTO
{
    public class TopicDto
    {
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "topic name length must be between 3 and 20")]
        public string name { get; set; }
    }
}
