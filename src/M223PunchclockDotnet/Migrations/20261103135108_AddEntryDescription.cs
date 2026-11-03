using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace M223PunchclockDotnet.Migrations
{
    /// <inheritdoc />
    public partial class AddEntryDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Entries",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Entries");
        }
    }
}
