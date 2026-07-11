using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.DTOs.Projects
{
    public class UpdateProjectDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = default!;

        [MaxLength(500)]
        public string? Description { get; set; }
    }
}