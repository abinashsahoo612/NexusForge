using System.ComponentModel.DataAnnotations;
using TaskManagementSystem.Enums.Workspaces;

namespace TaskManagementSystem.DTOs.Workspaces
{
    public class CreateWorkspaceDto
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = default!;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public MembershipPolicy MembershipPolicy { get; set; }
    }
}