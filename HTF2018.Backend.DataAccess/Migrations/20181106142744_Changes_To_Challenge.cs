using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HTF2018.Backend.DataAccess.Migrations
{
    public partial class Changes_To_Challenge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SolvedOn",
                table: "Challenges",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Challenges",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SolvedOn",
                table: "Challenges");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Challenges");
        }
    }
}
