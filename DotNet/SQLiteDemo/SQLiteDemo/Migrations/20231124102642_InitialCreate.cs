using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SQLiteDemo.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    InActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    DOB = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Sex = table.Column<int>(type: "INTEGER", nullable: false),
                    Sexuality = table.Column<int>(type: "INTEGER", nullable: false),
                    Weight = table.Column<int>(type: "INTEGER", nullable: false),
                    Height = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PeopleRelations",
                columns: table => new
                {
                    PersonAId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PersonBId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeopleRelations", x => x.PersonAId);
                    table.ForeignKey(
                        name: "FK_PeopleRelations_People_PersonAId",
                        column: x => x.PersonAId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeopleRelations_People_PersonBId",
                        column: x => x.PersonBId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PeopleRelations_PersonBId",
                table: "PeopleRelations",
                column: "PersonBId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PeopleRelations");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
