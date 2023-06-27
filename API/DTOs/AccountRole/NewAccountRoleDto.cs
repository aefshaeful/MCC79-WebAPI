using System.ComponentModel.DataAnnotations;

namespace API.DTOs.AccountRole
{
    public class NewAccountRoleDto
    {
        [Required]
        public Guid AccountGuid { get; set; }
        [Required]
        public Guid RoleGuid { get; set; }
    }
}
