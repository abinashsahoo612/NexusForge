using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManagementSystem.Enums.Workspaces;
using TaskManagementSystem.Models.Identity;

namespace TaskManagementSystem.Models.Workspaces
{
    public class WorkspaceMember
    {
        public int Id { get; set; }

        [Required]
        public int WorkspaceId { get; set; }

        [ForeignKey(nameof(WorkspaceId))]
        public Workspace Workspace { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser? User { get; set; }

        [Required]
        public WorkspaceRole Role { get; set; } = WorkspaceRole.Member;

        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;
    }
}