using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseSource.Data.Migrations
{
    public partial class add_col_Status_tb_VoteProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Down",
                table: "VoteProjects");

            migrationBuilder.DropColumn(
                name: "Up",
                table: "VoteProjects");

            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "VoteProjects",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: "c1105ce5-9dbc-49a9-a7d5-c963b6daa62a",
                column: "ConcurrencyStamp",
                value: "4f377c8a-29c4-4bae-a1ee-631fb733f487");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: "ffded6b0-3769-4976-841b-69459049a62d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9e5e40f1-35bd-471e-a4ca-7ee5808eb9d9", "AQAAAAEAACcQAAAAEB47mxp9IZUTqNRR2NiZVIuHJr8fbj21MWH6M5ASpBu8pdTR7j1O3Cthkv39zKgYfg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "VoteProjects");

            migrationBuilder.AddColumn<bool>(
                name: "Down",
                table: "VoteProjects",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Up",
                table: "VoteProjects",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: "c1105ce5-9dbc-49a9-a7d5-c963b6daa62a",
                column: "ConcurrencyStamp",
                value: "5ac6e26a-e1e7-4669-9890-3294ae4bb1ec");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: "ffded6b0-3769-4976-841b-69459049a62d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "557149af-df56-4f06-a3cc-d810e733b4f9", "AQAAAAEAACcQAAAAEPr/WXq9J7enwijH4tpAwOYEz7mai90Iulvx+pB1Shpq6/YpsLrCqD1Ur7vX9i9Y/A==" });
        }
    }
}
