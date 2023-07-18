using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseSource.Data.Migrations
{
    public partial class add_tb_PetProject_and_Category : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryProjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Published = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryProjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PetProjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryProjectId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkDemo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LinkSourceCode = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Published = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetProjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PetProjects_CategoryProjects_CategoryProjectId",
                        column: x => x.CategoryProjectId,
                        principalTable: "CategoryProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_PetProjects_CategoryProjectId",
                table: "PetProjects",
                column: "CategoryProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PetProjects");

            migrationBuilder.DropTable(
                name: "CategoryProjects");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: "c1105ce5-9dbc-49a9-a7d5-c963b6daa62a",
                column: "ConcurrencyStamp",
                value: "ae17f2b4-8a2c-4ab3-a851-71b61c6769f0");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: "ffded6b0-3769-4976-841b-69459049a62d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "406ba5c6-2bba-4afb-aaac-f695f85424a4", "AQAAAAEAACcQAAAAEBUnHhZXk2hkWXm4ARxA0GJi7ygIIU0mTJYjYFOVV0XI0UCyEcJ2gs5yVrCxIVFHBw==" });
        }
    }
}
