using System.ComponentModel.DataAnnotations;
using TaskManagementSystem.Enums.Workspaces;

namespace TaskManagementSystem.DTOs.Workspaces
{
    public class AddWorkspaceMemberDto
    {
        [Required]
        public int WorkspaceId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        public WorkspaceRole Role { get; set; }
    }
}