using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TESTT.Migrations
{
    public partial class idnetiyUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK__User_Tab__B9BE370F555B4770",
                table: "User_Table");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "User_Table",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "user_status",
                table: "User_Table",
                newName: "UserStatus");

            migrationBuilder.RenameColumn(
                name: "user_sleeptime",
                table: "User_Table",
                newName: "UserSleeptime");

            migrationBuilder.RenameColumn(
                name: "user_productivity_time",
                table: "User_Table",
                newName: "UserProductivityTime");

            migrationBuilder.RenameColumn(
                name: "user_password",
                table: "User_Table",
                newName: "UserPassword");

            migrationBuilder.RenameColumn(
                name: "user_lastname",
                table: "User_Table",
                newName: "UserLastname");

            migrationBuilder.RenameColumn(
                name: "user_firstname",
                table: "User_Table",
                newName: "UserFirstname");

            migrationBuilder.RenameColumn(
                name: "user_email",
                table: "User_Table",
                newName: "UserEmail");

            migrationBuilder.RenameColumn(
                name: "user_country",
                table: "User_Table",
                newName: "UserCountry");

            migrationBuilder.RenameColumn(
                name: "user_birthdate",
                table: "User_Table",
                newName: "UserBirthdate");

            migrationBuilder.RenameColumn(
                name: "user_age",
                table: "User_Table",
                newName: "UserAge");

            migrationBuilder.RenameColumn(
                name: "user_activities",
                table: "User_Table",
                newName: "UserActivities");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "User_Table",
                newName: "UserId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "User_Table",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User_Table",
                table: "User_Table",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_User_Table",
                table: "User_Table");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "User_Table",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "UserStatus",
                table: "User_Table",
                newName: "user_status");

            migrationBuilder.RenameColumn(
                name: "UserSleeptime",
                table: "User_Table",
                newName: "user_sleeptime");

            migrationBuilder.RenameColumn(
                name: "UserProductivityTime",
                table: "User_Table",
                newName: "user_productivity_time");

            migrationBuilder.RenameColumn(
                name: "UserPassword",
                table: "User_Table",
                newName: "user_password");

            migrationBuilder.RenameColumn(
                name: "UserLastname",
                table: "User_Table",
                newName: "user_lastname");

            migrationBuilder.RenameColumn(
                name: "UserFirstname",
                table: "User_Table",
                newName: "user_firstname");

            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "User_Table",
                newName: "user_email");

            migrationBuilder.RenameColumn(
                name: "UserCountry",
                table: "User_Table",
                newName: "user_country");

            migrationBuilder.RenameColumn(
                name: "UserBirthdate",
                table: "User_Table",
                newName: "user_birthdate");

            migrationBuilder.RenameColumn(
                name: "UserAge",
                table: "User_Table",
                newName: "user_age");

            migrationBuilder.RenameColumn(
                name: "UserActivities",
                table: "User_Table",
                newName: "user_activities");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "User_Table",
                newName: "user_id");

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "User_Table",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK__User_Tab__B9BE370F555B4770",
                table: "User_Table",
                column: "user_id");
        }
    }
}
