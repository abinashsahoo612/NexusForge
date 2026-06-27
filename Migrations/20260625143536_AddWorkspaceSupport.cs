using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TaskManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkspaceSupport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkspaceId",
                table: "Tasks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkspaceId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Workspace",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workspace", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_WorkspaceId",
                table: "Tasks",
                column: "WorkspaceId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_WorkspaceId",
                table: "AspNetUsers",
                column: "WorkspaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Workspace_WorkspaceId",
                table: "AspNetUsers",
                column: "WorkspaceId",
                principalTable: "Workspace",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Workspace_WorkspaceId",
                table: "Tasks",
                column: "WorkspaceId",
                principalTable: "Workspace",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Workspace_WorkspaceId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Workspace_WorkspaceId",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "Workspace");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_WorkspaceId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_WorkspaceId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WorkspaceId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "WorkspaceId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
