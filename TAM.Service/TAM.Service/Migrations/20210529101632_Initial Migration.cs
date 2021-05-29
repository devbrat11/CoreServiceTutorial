using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TAM.Service.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    SerialNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HostName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.SerialNumber);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SessionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentTeam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manager = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lead = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "UserCredentials",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCredentials", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    EmailId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Team = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.EmailId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "UserCredentials");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
