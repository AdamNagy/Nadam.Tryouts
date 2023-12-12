using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SQLiteDemo.Migrations
{
    public partial class v1_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "PeopleRelations");

            migrationBuilder.DropColumn(
                name: "InActive",
                table: "People");

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "People");

            migrationBuilder.DropColumn(
                name: "Sexuality",
                table: "People");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "PeopleRelations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "InActive",
                table: "People",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Sex",
                table: "People",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sexuality",
                table: "People",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
