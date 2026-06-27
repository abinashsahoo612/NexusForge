using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Models.Task;
using TaskManagementSystem.Models.Identity;
using TaskManagementSystem.Models.Workspaces;

namespace TaskManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
            public DbSet<TaskItem> Tasks { get; set; }
            public DbSet<Workspace> Workspaces { get; set; }
            public DbSet<WorkspaceMember> WorkspaceMembers { get; set; }
    }
}