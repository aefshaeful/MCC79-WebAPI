using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Role
{
    public class NewRoleDto
    {
        [Required]
        public string Name { get; set; }
    }
}
