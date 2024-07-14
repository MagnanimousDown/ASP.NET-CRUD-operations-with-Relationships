using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddClassroomAndUpdateStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Standard",
                table: "StudentRegister",
                newName: "PhoneNumber");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "StudentRegister",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ClassroomId",
                table: "StudentRegister",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "StudentRegister",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Classrooms",
                columns: table => new
                {
                    ClassroomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Division = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeacherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classrooms", x => x.ClassroomId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentRegister_ClassroomId",
                table: "StudentRegister",
                column: "ClassroomId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentRegister_Classrooms_ClassroomId",
                table: "StudentRegister",
                column: "ClassroomId",
                principalTable: "Classrooms",
                principalColumn: "ClassroomId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentRegister_Classrooms_ClassroomId",
                table: "StudentRegister");

            migrationBuilder.DropTable(
                name: "Classrooms");

            migrationBuilder.DropIndex(
                name: "IX_StudentRegister_ClassroomId",
                table: "StudentRegister");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "StudentRegister");

            migrationBuilder.DropColumn(
                name: "ClassroomId",
                table: "StudentRegister");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "StudentRegister");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "StudentRegister",
                newName: "Standard");
        }
    }
}
