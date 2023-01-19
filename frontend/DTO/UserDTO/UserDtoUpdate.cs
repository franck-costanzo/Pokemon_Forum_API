using System.ComponentModel.DataAnnotations;

namespace Smogon_MAUIapp.DTO.UserDTO
{
    public class UserDtoUpdate
    {
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "username length must be between 3 and 20")]
        public string username { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "password length must be between 8 and 20")]
        public string oldPassword { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "password length must be between 8 and 20")]
        public string password { get; set; }

        [Required]
        public string email { get; set; }
    }
}
