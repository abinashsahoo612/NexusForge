using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddPermissionSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkspaceRolePermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkspaceId = table.Column<int>(type: "integer", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    PermissionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkspaceRolePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkspaceRolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkspaceRolePermissions_Workspaces_WorkspaceId",
                        column: x => x.WorkspaceId,
                        principalTable: "Workspaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "View workspace", "ViewWorkspace" },
                    { 2, "Edit workspace", "EditWorkspace" },
                    { 3, "Add, remove and manage workspace members", "ManageMembers" },
                    { 4, "Manage workspace role permissions", "ManageRoles" },
                    { 5, "View projects", "ViewProject" },
                    { 6, "Create projects", "CreateProject" },
                    { 7, "Edit projects", "EditProject" },
                    { 8, "Delete projects", "DeleteProject" },
                    { 9, "View tasks", "ViewTask" },
                    { 10, "Create tasks", "CreateTask" },
                    { 11, "Edit tasks", "EditTask" },
                    { 12, "Delete tasks", "DeleteTask" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkspaceRolePermissions_PermissionId",
                table: "WorkspaceRolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkspaceRolePermissions_WorkspaceId",
                table: "WorkspaceRolePermissions",
                column: "WorkspaceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkspaceRolePermissions");

            migrationBuilder.DropTable(
                name: "Permissions");
        }
    }
}
