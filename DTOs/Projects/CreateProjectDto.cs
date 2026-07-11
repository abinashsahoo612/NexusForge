using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.DTOs.Projects
{
    public class CreateProjectDto
    {
        [Required]
        public int WorkspaceId { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }
    }
}