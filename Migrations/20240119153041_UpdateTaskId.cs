using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TESTT.Migrations
{
    public partial class UpdateTaskId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User_Table",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false),
                    username = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    user_email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    user_password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    user_birthdate = table.Column<DateTime>(type: "date", nullable: true),
                    user_firstname = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    user_lastname = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    user_country = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    user_age = table.Column<int>(type: "int", nullable: true),
                    user_activities = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    user_status = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    user_sleeptime = table.Column<TimeSpan>(type: "time", nullable: true),
                    user_productivity_time = table.Column<TimeSpan>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User_Tab__B9BE370F555B4770", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "List",
                columns: table => new
                {
                    list_id = table.Column<int>(type: "int", nullable: false),
                    list_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    list_category = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    user_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_List", x => x.list_id);
                    table.ForeignKey(
                        name: "FK__List__user_id__398D8EEE",
                        column: x => x.user_id,
                        principalTable: "User_Table",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    project_id = table.Column<int>(type: "int", nullable: false),
                    project_title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    project_no_of_tasks = table.Column<int>(type: "int", nullable: true),
                    project_no_of_completed = table.Column<int>(type: "int", nullable: true),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    list_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.project_id);
                    table.ForeignKey(
                        name: "FK__Project__list_id__3D5E1FD2",
                        column: x => x.list_id,
                        principalTable: "List",
                        principalColumn: "list_id");
                    table.ForeignKey(
                        name: "FK__Project__user_id__3C69FB99",
                        column: x => x.user_id,
                        principalTable: "User_Table",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    TaskTitle = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    TaskPriority = table.Column<int>(type: "int", nullable: true),
                    TaskTag = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    TaskDate = table.Column<DateTime>(type: "date", nullable: true),
                    TaskDuration = table.Column<TimeSpan>(type: "time", nullable: true),
                    TaskStartTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    TaskEndTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    TaskActualDuration = table.Column<TimeSpan>(type: "time", nullable: true),
                    TaskDifficulty = table.Column<int>(type: "int", nullable: true),
                    TaskStatus = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    ListId = table.Column<int>(type: "int", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK__Tasks__list_id__412EB0B6",
                        column: x => x.ListId,
                        principalTable: "List",
                        principalColumn: "list_id");
                    table.ForeignKey(
                        name: "FK__Tasks__project_i__4222D4EF",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "project_id");
                    table.ForeignKey(
                        name: "FK__Tasks__user_id__403A8C7D",
                        column: x => x.UserId,
                        principalTable: "User_Table",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_List_user_id",
                table: "List",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Project_list_id",
                table: "Project",
                column: "list_id");

            migrationBuilder.CreateIndex(
                name: "IX_Project_user_id",
                table: "Project",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ListId",
                table: "Tasks",
                column: "ListId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ProjectId",
                table: "Tasks",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UserId",
                table: "Tasks",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "List");

            migrationBuilder.DropTable(
                name: "User_Table");
        }
    }
}
