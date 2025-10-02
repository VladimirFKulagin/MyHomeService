using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyHomeService.Migrations
{
    /// <inheritdoc />
    public partial class RenameIsCompleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isCompleted",
                table: "TaskItems",
                newName: "IsCompleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsCompleted",
                table: "TaskItems",
                newName: "isCompleted");
        }
    }
}
