using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseSource.Data.Migrations
{
    public partial class update_maxlength_col_IP_tb_VoteProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoteProject_PetProjects_PetProjectId",
                table: "VoteProject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VoteProject",
                table: "VoteProject");

            migrationBuilder.RenameTable(
                name: "VoteProject",
                newName: "VoteProjects");

            migrationBuilder.RenameIndex(
                name: "IX_VoteProject_PetProjectId",
                table: "VoteProjects",
                newName: "IX_VoteProjects_PetProjectId");

            migrationBuilder.AlterColumn<string>(
                name: "IP",
                table: "VoteProjects",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VoteProjects",
                table: "VoteProjects",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_VoteProjects_PetProjects_PetProjectId",
                table: "VoteProjects",
                column: "PetProjectId",
                principalTable: "PetProjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoteProjects_PetProjects_PetProjectId",
                table: "VoteProjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VoteProjects",
                table: "VoteProjects");

            migrationBuilder.RenameTable(
                name: "VoteProjects",
                newName: "VoteProject");

            migrationBuilder.RenameIndex(
                name: "IX_VoteProjects_PetProjectId",
                table: "VoteProject",
                newName: "IX_VoteProject_PetProjectId");

            migrationBuilder.AlterColumn<string>(
                name: "IP",
                table: "VoteProject",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VoteProject",
                table: "VoteProject",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_VoteProject_PetProjects_PetProjectId",
                table: "VoteProject",
                column: "PetProjectId",
                principalTable: "PetProjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
