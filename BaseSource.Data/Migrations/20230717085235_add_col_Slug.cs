using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseSource.Data.Migrations
{
    public partial class add_col_Slug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "PetProjects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "CategoryProjects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: "c1105ce5-9dbc-49a9-a7d5-c963b6daa62a",
                column: "ConcurrencyStamp",
                value: "f6b8d682-2e67-40de-b67c-1410e39cf869");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: "ffded6b0-3769-4976-841b-69459049a62d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5ea40646-8bfd-49a7-aceb-08e7925a1500", "AQAAAAEAACcQAAAAEBbIx+TdFYk8guC+4XlSrzz8GiESHgafGH4vJ5josbUkqt3n41A1uc1BdVYx5Mj+ow==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slug",
                table: "PetProjects");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "CategoryProjects");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: "c1105ce5-9dbc-49a9-a7d5-c963b6daa62a",
                column: "ConcurrencyStamp",
                value: "152becd7-e38f-45ed-be85-a16cdee278c5");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: "ffded6b0-3769-4976-841b-69459049a62d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4331b681-f647-4fd9-b01b-1c45a01b91da", "AQAAAAEAACcQAAAAEKDLzu5jglAKxSNKxoxwELrMDjRFxRCq+7uebw/rW67EhS6uorljg1ei6mgAwGhqAQ==" });
        }
    }
}
