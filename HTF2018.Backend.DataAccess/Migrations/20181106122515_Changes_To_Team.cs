using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HTF2018.Backend.DataAccess.Migrations
{
    public partial class Changes_To_Team : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Teams",
                newName: "Identification");

            migrationBuilder.AddColumn<string>(
                name: "HashedSecret",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "Challenges",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Identifier",
                table: "Challenges",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Question",
                table: "Challenges",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Statistics",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SysId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TeamId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statistics", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Statistics_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_SysId",
                table: "Statistics",
                column: "SysId",
                unique: true)
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_TeamId",
                table: "Statistics",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Statistics");

            migrationBuilder.DropColumn(
                name: "HashedSecret",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Answer",
                table: "Challenges");

            migrationBuilder.DropColumn(
                name: "Identifier",
                table: "Challenges");

            migrationBuilder.DropColumn(
                name: "Question",
                table: "Challenges");

            migrationBuilder.RenameColumn(
                name: "Identification",
                table: "Teams",
                newName: "PasswordHash");
        }
    }
}
