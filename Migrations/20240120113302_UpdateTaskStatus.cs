using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TESTT.Migrations
{
    public partial class UpdateTaskStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "TaskStatus",
                table: "Tasks",
                type: "bit",
                unicode: false,
                maxLength: 255,
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TaskId",
                table: "Tasks",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TaskStatus",
                table: "Tasks",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<int>(
                name: "TaskId",
                table: "Tasks",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }
    }
}
