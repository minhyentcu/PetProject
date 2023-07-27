using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseSource.Data.Migrations
{
    public partial class add_tb_VoteProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VoteProject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PetProjectId = table.Column<int>(type: "int", nullable: false),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Up = table.Column<bool>(type: "bit", nullable: false),
                    Down = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoteProject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoteProject_PetProjects_PetProjectId",
                        column: x => x.PetProjectId,
                        principalTable: "PetProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: "c1105ce5-9dbc-49a9-a7d5-c963b6daa62a",
                column: "ConcurrencyStamp",
                value: "edc6f0bf-ce46-47b4-9452-ffbcfda443f8");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: "ffded6b0-3769-4976-841b-69459049a62d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b0a1644b-9a29-46cd-9045-a6811a1282dc", "AQAAAAEAACcQAAAAEOnDwhbH+z22isTHzKn/FxpOijKL6BA7GtFVvNy4p02KCS2KBPa+zvOEnJLXvN9CzA==" });

            migrationBuilder.CreateIndex(
                name: "IX_VoteProject_PetProjectId",
                table: "VoteProject",
                column: "PetProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VoteProject");

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
    }
}
