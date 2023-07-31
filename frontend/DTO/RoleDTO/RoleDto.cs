using System.ComponentModel.DataAnnotations;

namespace Smogon_MAUIapp.DTO.RoleDTO
{
    public class RoleDto
    {
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "username length must be between 3 and 20")]
        public string name { get; set; }
        [Required]
        public string description { get; set; }
    }
}
