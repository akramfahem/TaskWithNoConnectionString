using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APII.Migrations
{
    /// <inheritdoc />
    public partial class onetomanyandfixingsomestuff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Doctors_DoctorId",
                table: "Subjects");

            migrationBuilder.RenameColumn(
                name: "StudentGrade",
                table: "Subjects",
                newName: "CollegeId");

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "Subjects",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "Students",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_CollegeId",
                table: "Subjects",
                column: "CollegeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Colleges_CollegeId",
                table: "Subjects",
                column: "CollegeId",
                principalTable: "Colleges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Doctors_DoctorId",
                table: "Subjects",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Colleges_CollegeId",
                table: "Subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Doctors_DoctorId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_CollegeId",
                table: "Subjects");

            migrationBuilder.RenameColumn(
                name: "CollegeId",
                table: "Subjects",
                newName: "StudentGrade");

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "Subjects",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Age",
                table: "Students",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Age",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Doctors_DoctorId",
                table: "Subjects",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
