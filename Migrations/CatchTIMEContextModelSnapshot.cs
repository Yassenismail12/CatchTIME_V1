﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TESTT.Models;

#nullable disable

namespace TESTT.Migrations
{
    [DbContext(typeof(CatchTIMEContext))]
    partial class CatchTIMEContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TESTT.Models.List", b =>
                {
                    b.Property<int>("ListId")
                        .HasColumnType("int")
                        .HasColumnName("ListId");

                    b.Property<string>("ListCategory")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("ListCategory");

                    b.Property<string>("ListTitle")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("ListTitle");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.HasKey("ListId");

                    b.HasIndex("UserId");

                    b.ToTable("List", (string)null);
                });

            modelBuilder.Entity("TESTT.Models.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .HasColumnType("int")
                        .HasColumnName("ProjectId");

                    b.Property<int?>("ListId")
                        .HasColumnType("int")
                        .HasColumnName("ListId");

                    b.Property<int?>("ProjectNoOfCompleted")
                        .HasColumnType("int")
                        .HasColumnName("ProjectNoOfCompleted");

                    b.Property<int?>("ProjectNoOfTasks")
                        .HasColumnType("int")
                        .HasColumnName("ProjectNoOfTasks");

                    b.Property<string>("ProjectTitle")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("ProjectTitle");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.HasKey("ProjectId");

                    b.HasIndex("ListId");

                    b.HasIndex("UserId");

                    b.ToTable("Project", (string)null);
                });

            modelBuilder.Entity("TESTT.Models.Task", b =>
                {
                    b.Property<int>("TaskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("TaskId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TaskId"), 1L, 1);

                    b.Property<int?>("ListId")
                        .HasColumnType("int")
                        .HasColumnName("ListId");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int")
                        .HasColumnName("ProjectId");

                    b.Property<TimeSpan?>("TaskActualDuration")
                        .HasColumnType("time")
                        .HasColumnName("TaskActualDuration");

                    b.Property<DateTime?>("TaskDate")
                        .HasColumnType("date")
                        .HasColumnName("TaskDate");

                    b.Property<int?>("TaskDifficulty")
                        .HasColumnType("int")
                        .HasColumnName("TaskDifficulty");

                    b.Property<TimeSpan?>("TaskDuration")
                        .HasColumnType("time")
                        .HasColumnName("TaskDuration");

                    b.Property<TimeSpan?>("TaskEndTime")
                        .HasColumnType("time")
                        .HasColumnName("TaskEndTime");

                    b.Property<int?>("TaskPriority")
                        .HasColumnType("int")
                        .HasColumnName("TaskPriority");

                    b.Property<TimeSpan?>("TaskStartTime")
                        .HasColumnType("time")
                        .HasColumnName("TaskStartTime");

                    b.Property<bool>("TaskStatus")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("bit")
                        .HasColumnName("TaskStatus");

                    b.Property<string>("TaskTag")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("TaskTag");

                    b.Property<string>("TaskTitle")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("TaskTitle");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.HasKey("TaskId");

                    b.HasIndex("ListId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("TESTT.Models.UserTable", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.Property<string>("UserActivities")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("user_activities");

                    b.Property<int?>("UserAge")
                        .HasColumnType("int")
                        .HasColumnName("user_age");

                    b.Property<DateTime?>("UserBirthdate")
                        .HasColumnType("date")
                        .HasColumnName("user_birthdate");

                    b.Property<string>("UserCountry")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("user_country");

                    b.Property<string>("UserEmail")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("user_email");

                    b.Property<string>("UserFirstname")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("user_firstname");

                    b.Property<string>("UserLastname")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("user_lastname");

                    b.Property<string>("UserPassword")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("user_password");

                    b.Property<TimeSpan?>("UserProductivityTime")
                        .HasColumnType("time")
                        .HasColumnName("user_productivity_time");

                    b.Property<TimeSpan?>("UserSleeptime")
                        .HasColumnType("time")
                        .HasColumnName("user_sleeptime");

                    b.Property<string>("UserStatus")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("user_status");

                    b.Property<string>("Username")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("username");

                    b.HasKey("UserId")
                        .HasName("PK__User_Tab__B9BE370F555B4770");

                    b.ToTable("User_Table", (string)null);
                });

            modelBuilder.Entity("TESTT.Models.List", b =>
                {
                    b.HasOne("TESTT.Models.UserTable", "User")
                        .WithMany("Lists")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__List__user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TESTT.Models.Project", b =>
                {
                    b.HasOne("TESTT.Models.List", "List")
                        .WithMany("Projects")
                        .HasForeignKey("ListId")
                        .HasConstraintName("FK__Project__list_id");

                    b.HasOne("TESTT.Models.UserTable", "User")
                        .WithMany("Projects")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Project__user_id");

                    b.Navigation("List");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TESTT.Models.Task", b =>
                {
                    b.HasOne("TESTT.Models.List", "List")
                        .WithMany("Tasks")
                        .HasForeignKey("ListId")
                        .HasConstraintName("FK__Tasks__list_id__412EB0B6");

                    b.HasOne("TESTT.Models.Project", "Project")
                        .WithMany("Tasks")
                        .HasForeignKey("ProjectId")
                        .HasConstraintName("FK__Tasks__project_i__4222D4EF");

                    b.HasOne("TESTT.Models.UserTable", "User")
                        .WithMany("Tasks")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Tasks__user_id__403A8C7D");

                    b.Navigation("List");

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TESTT.Models.List", b =>
                {
                    b.Navigation("Projects");

                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("TESTT.Models.Project", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("TESTT.Models.UserTable", b =>
                {
                    b.Navigation("Lists");

                    b.Navigation("Projects");

                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}
