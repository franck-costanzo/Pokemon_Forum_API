using System.ComponentModel.DataAnnotations;

namespace Pokemon_Forum_API.DTO.UserDTO
{
    public class UserDtoUpdateAvatar
    {
        [Required]
        public string avatar_url { get; set; }
    }
}
