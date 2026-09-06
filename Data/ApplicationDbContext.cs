using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Models.Task;
using TaskManagementSystem.Models.Identity;
using TaskManagementSystem.Models.Workspaces;
using TaskManagementSystem.Models.Activity;
using TaskManagementSystem.Models.Permissions;

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
            public DbSet<Project> Project { get; set; }
            public DbSet<ActivityLog> ActivityLogs { get; set; }
            public DbSet<Permission> Permissions { get; set; }
            public DbSet<WorkspaceRolePermission> WorkspaceRolePermissions { get; set; }

            protected override void OnModelCreating(ModelBuilder builder)
            {
                base.OnModelCreating(builder);

                builder.Entity<Permission>().HasData(
                    new Permission
                    {
                        Id = 1,
                        Name = "ViewWorkspace",
                        Description = "View workspace"
                    },
                    new Permission
                    {
                        Id = 2,
                        Name = "EditWorkspace",
                        Description = "Edit workspace"
                    },
                    new Permission
                    {
                        Id = 3,
                        Name = "ManageMembers",
                        Description = "Add, remove and manage workspace members"
                    },
                    new Permission
                    {
                        Id = 4,
                        Name = "ManageRoles",
                        Description = "Manage workspace role permissions"
                    },
                    new Permission
                    {
                        Id = 5,
                        Name = "ViewProject",
                        Description = "View projects"
                    },
                    new Permission
                    {
                        Id = 6,
                        Name = "CreateProject",
                        Description = "Create projects"
                    },
                    new Permission
                    {
                        Id = 7,
                        Name = "EditProject",
                        Description = "Edit projects"
                    },
                    new Permission
                    {
                        Id = 8,
                        Name = "DeleteProject",
                        Description = "Delete projects"
                    },
                    new Permission
                    {
                        Id = 9,
                        Name = "ViewTask",
                        Description = "View tasks"
                    },
                    new Permission
                    {
                        Id = 10,
                        Name = "CreateTask",
                        Description = "Create tasks"
                    },
                    new Permission
                    {
                        Id = 11,
                        Name = "EditTask",
                        Description = "Edit tasks"
                    },
                    new Permission
                    {
                        Id = 12,
                        Name = "DeleteTask",
                        Description = "Delete tasks"
                    }
                );
            }
    }
}