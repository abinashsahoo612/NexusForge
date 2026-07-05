using System.ComponentModel.DataAnnotations;
using TaskManagementSystem.Enums.Workspaces;

namespace TaskManagementSystem.DTOs.Workspaces
{
    public class AddWorkspaceMemberDto
    {
        [Required]
        public int WorkspaceId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public WorkspaceRole Role { get; set; } = WorkspaceRole.Member;
    }
}