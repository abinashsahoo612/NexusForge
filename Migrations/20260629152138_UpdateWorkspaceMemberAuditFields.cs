using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWorkspaceMemberAuditFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddedByUserId",
                table: "WorkspaceMembers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_WorkspaceMembers_AddedByUserId",
                table: "WorkspaceMembers",
                column: "AddedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkspaceMembers_AspNetUsers_AddedByUserId",
                table: "WorkspaceMembers",
                column: "AddedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkspaceMembers_AspNetUsers_AddedByUserId",
                table: "WorkspaceMembers");

            migrationBuilder.DropIndex(
                name: "IX_WorkspaceMembers_AddedByUserId",
                table: "WorkspaceMembers");

            migrationBuilder.DropColumn(
                name: "AddedByUserId",
                table: "WorkspaceMembers");
        }
    }
}
