using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TESTT.Migrations
{
    public partial class UpdateListId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__List__user_id__398D8EEE",
                table: "List");

            migrationBuilder.DropForeignKey(
                name: "FK__Project__list_id__3D5E1FD2",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK__Project__user_id__3C69FB99",
                table: "Project");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Project",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "project_title",
                table: "Project",
                newName: "ProjectTitle");

            migrationBuilder.RenameColumn(
                name: "project_no_of_tasks",
                table: "Project",
                newName: "ProjectNoOfTasks");

            migrationBuilder.RenameColumn(
                name: "project_no_of_completed",
                table: "Project",
                newName: "ProjectNoOfCompleted");

            migrationBuilder.RenameColumn(
                name: "list_id",
                table: "Project",
                newName: "ListId");

            migrationBuilder.RenameColumn(
                name: "project_id",
                table: "Project",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Project_user_id",
                table: "Project",
                newName: "IX_Project_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Project_list_id",
                table: "Project",
                newName: "IX_Project_ListId");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "List",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "list_category",
                table: "List",
                newName: "ListCategory");

            migrationBuilder.RenameColumn(
                name: "list_id",
                table: "List",
                newName: "ListId");

            migrationBuilder.RenameColumn(
                name: "list_name",
                table: "List",
                newName: "ListTitle");

            migrationBuilder.RenameIndex(
                name: "IX_List_user_id",
                table: "List",
                newName: "IX_List_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK__List__user_id",
                table: "List",
                column: "UserId",
                principalTable: "User_Table",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK__Project__list_id",
                table: "Project",
                column: "ListId",
                principalTable: "List",
                principalColumn: "ListId");

            migrationBuilder.AddForeignKey(
                name: "FK__Project__user_id",
                table: "Project",
                column: "UserId",
                principalTable: "User_Table",
                principalColumn: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__List__user_id",
                table: "List");

            migrationBuilder.DropForeignKey(
                name: "FK__Project__list_id",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK__Project__user_id",
                table: "Project");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Project",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "ProjectTitle",
                table: "Project",
                newName: "project_title");

            migrationBuilder.RenameColumn(
                name: "ProjectNoOfTasks",
                table: "Project",
                newName: "project_no_of_tasks");

            migrationBuilder.RenameColumn(
                name: "ProjectNoOfCompleted",
                table: "Project",
                newName: "project_no_of_completed");

            migrationBuilder.RenameColumn(
                name: "ListId",
                table: "Project",
                newName: "list_id");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "Project",
                newName: "project_id");

            migrationBuilder.RenameIndex(
                name: "IX_Project_UserId",
                table: "Project",
                newName: "IX_Project_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_Project_ListId",
                table: "Project",
                newName: "IX_Project_list_id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "List",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "ListCategory",
                table: "List",
                newName: "list_category");

            migrationBuilder.RenameColumn(
                name: "ListId",
                table: "List",
                newName: "list_id");

            migrationBuilder.RenameColumn(
                name: "ListTitle",
                table: "List",
                newName: "list_name");

            migrationBuilder.RenameIndex(
                name: "IX_List_UserId",
                table: "List",
                newName: "IX_List_user_id");

            migrationBuilder.AddForeignKey(
                name: "FK__List__user_id__398D8EEE",
                table: "List",
                column: "user_id",
                principalTable: "User_Table",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK__Project__list_id__3D5E1FD2",
                table: "Project",
                column: "list_id",
                principalTable: "List",
                principalColumn: "list_id");

            migrationBuilder.AddForeignKey(
                name: "FK__Project__user_id__3C69FB99",
                table: "Project",
                column: "user_id",
                principalTable: "User_Table",
                principalColumn: "user_id");
        }
    }
}
