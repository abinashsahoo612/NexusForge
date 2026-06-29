using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManagementSystem.Enums.Workspaces;
using TaskManagementSystem.Models.Identity;

namespace TaskManagementSystem.Models.Workspaces
{
    public class Workspace
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public MembershipPolicy MembershipPolicy { get; set; } = MembershipPolicy.Flexible;

        // User who created this workspace
        [Required]
        public string CreatedByUserId { get; set; } = string.Empty;

        [ForeignKey(nameof(CreatedByUserId))]
        public ApplicationUser CreatedByUser { get; set; } = null!;

        public ICollection<WorkspaceMember> Members { get; set; } = new List<WorkspaceMember>();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public bool IsActive { get; set; } = true;
    }
}