using System.ComponentModel.DataAnnotations;

namespace Pokemon_Forum_API.DTO.RoleDTO
{
    public class RoleDtoUpdate
    {
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "username length must be between 3 and 20")]
        public string name { get; set; }

        [Required]
        public string description { get; set; }
    }
}
