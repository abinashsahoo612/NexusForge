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
        public Workspace Workspace { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;

        [Required]
        public WorkspaceRole Role { get; set; } = WorkspaceRole.Member;

        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public string AddedByUserId { get; set; } = string.Empty;

        [ForeignKey(nameof(AddedByUserId))]
        public ApplicationUser AddedByUser { get; set; } = null!;

        public bool IsActive { get; set; } = true;
    }
}