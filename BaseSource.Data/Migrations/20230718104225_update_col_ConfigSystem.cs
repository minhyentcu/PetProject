using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseSource.Data.Migrations
{
    public partial class update_col_ConfigSystem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankAccountName",
                table: "ConfigSystems");

            migrationBuilder.DropColumn(
                name: "BankName",
                table: "ConfigSystems");

            migrationBuilder.DropColumn(
                name: "BankNumber",
                table: "ConfigSystems");

            migrationBuilder.RenameColumn(
                name: "LinkFBAdmin",
                table: "ConfigSystems",
                newName: "WebsiteName");

            migrationBuilder.AddColumn<string>(
                name: "About",
                table: "ConfigSystems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkFB",
                table: "ConfigSystems",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "ConfigSystems",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: "c1105ce5-9dbc-49a9-a7d5-c963b6daa62a",
                column: "ConcurrencyStamp",
                value: "fc17e0f6-f8ef-45f4-8553-a5ab199d7e99");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: "ffded6b0-3769-4976-841b-69459049a62d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e6438326-9f60-4c12-9f91-9fa852c5cc33", "AQAAAAEAACcQAAAAEPrN0XhpT0nlm+JPKUZVx2b5T0iQEN4/rQFEr2krFEaJNY9mMqeXi0b/5fnCXLa2qg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "About",
                table: "ConfigSystems");

            migrationBuilder.DropColumn(
                name: "LinkFB",
                table: "ConfigSystems");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "ConfigSystems");

            migrationBuilder.RenameColumn(
                name: "WebsiteName",
                table: "ConfigSystems",
                newName: "LinkFBAdmin");

            migrationBuilder.AddColumn<string>(
                name: "BankAccountName",
                table: "ConfigSystems",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "ConfigSystems",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankNumber",
                table: "ConfigSystems",
                type: "nvarchar(50)",
                maxLength: 50,
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
    }
}
