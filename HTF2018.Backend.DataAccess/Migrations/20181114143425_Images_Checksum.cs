using Microsoft.EntityFrameworkCore.Migrations;

namespace HTF2018.Backend.DataAccess.Migrations
{
    public partial class Images_Checksum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Checksum",
                table: "Images",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_Checksum",
                table: "Images",
                column: "Checksum",
                unique: true,
                filter: "[Checksum] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Images_Checksum",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Checksum",
                table: "Images");
        }
    }
}
