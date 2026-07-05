using System.ComponentModel.DataAnnotations;
using TaskManagementSystem.Enums.Workspaces;

namespace TaskManagementSystem.DTOs.Workspaces
{
    public class CreateWorkspaceMemberDto
    {
        [Required]
        public int WorkspaceId { get; set; }

        [Required]
        public string FullName { get; set; } = default!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        public string Password { get; set; } = default!;

        [Required]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = default!;

        public WorkspaceRole Role { get; set; } = WorkspaceRole.Member;
    }
}