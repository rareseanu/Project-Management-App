using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManagementApp.Models.Database.Migrations
{
    public partial class AddTextToCommentary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Commentaries",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Text",
                table: "Commentaries");
        }
    }
}
