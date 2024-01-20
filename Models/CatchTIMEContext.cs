using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TESTT.Models
{
    public partial class CatchTIMEContext : DbContext
    {
        public CatchTIMEContext()
        {
        }

        public CatchTIMEContext(DbContextOptions<CatchTIMEContext> options)
            : base(options)
        {
        }

        public virtual DbSet<List> Lists { get; set; } = null!;
        public virtual DbSet<Project> Projects { get; set; } = null!;
        public virtual DbSet<Task> Tasks { get; set; } = null!;
        public virtual DbSet<UserTable> UserTables { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-32VL26T\\SQLEXPRESS;Database=CatchTime;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<List>(entity =>
            {
                entity.ToTable("List");

                entity.Property(e => e.ListId)
                    .ValueGeneratedNever()
                    .HasColumnName("list_id");

                entity.Property(e => e.ListCategory)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("list_category");

                entity.Property(e => e.ListTitle)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("list_title");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Lists)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__List__user_id__398D8EEE");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Project");

                entity.Property(e => e.ProjectId)
                    .ValueGeneratedNever()
                    .HasColumnName("project_id");

                entity.Property(e => e.ListId).HasColumnName("list_id");

                entity.Property(e => e.ProjectNoOfCompleted).HasColumnName("project_no_of_completed");

                entity.Property(e => e.ProjectNoOfTasks).HasColumnName("project_no_of_tasks");

                entity.Property(e => e.ProjectTitle)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("project_title");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.List)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.ListId)
                    .HasConstraintName("FK__Project__list_id__3D5E1FD2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Project__user_id__3C69FB99");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.Property(e => e.TaskId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("TaskId");

                entity.Property(e => e.ListId).HasColumnName("ListId");

                entity.Property(e => e.ProjectId).HasColumnName("ProjectId");

                entity.Property(e => e.TaskActualDuration).HasColumnName("TaskActualDuration");

                entity.Property(e => e.TaskDate)
                    .HasColumnType("date")
                    .HasColumnName("TaskDate");

                entity.Property(e => e.TaskDifficulty).HasColumnName("TaskDifficulty");

                entity.Property(e => e.TaskDuration).HasColumnName("TaskDuration");

                entity.Property(e => e.TaskEndTime).HasColumnName("TaskEndTime");

                entity.Property(e => e.TaskPriority).HasColumnName("TaskPriority");

                entity.Property(e => e.TaskStartTime).HasColumnName("TaskStartTime");

                entity.Property(e => e.TaskStatus)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TaskStatus");

                entity.Property(e => e.TaskTag)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TaskTag");

                entity.Property(e => e.TaskTitle)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TaskTitle");

                entity.Property(e => e.UserId).HasColumnName("UserId");

                entity.HasOne(d => d.List)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.ListId)
                    .HasConstraintName("FK__Tasks__list_id__412EB0B6");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK__Tasks__project_i__4222D4EF");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Tasks__user_id__403A8C7D");
            });

            modelBuilder.Entity<UserTable>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__User_Tab__B9BE370F555B4770");

                entity.ToTable("User_Table");

                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("user_id");

                entity.Property(e => e.UserActivities)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("user_activities");

                entity.Property(e => e.UserAge).HasColumnName("user_age");

                entity.Property(e => e.UserBirthdate)
                    .HasColumnType("date")
                    .HasColumnName("user_birthdate");

                entity.Property(e => e.UserCountry)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("user_country");

                entity.Property(e => e.UserEmail)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("user_email");

                entity.Property(e => e.UserFirstname)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("user_firstname");

                entity.Property(e => e.UserLastname)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("user_lastname");

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("user_password");

                entity.Property(e => e.UserProductivityTime).HasColumnName("user_productivity_time");

                entity.Property(e => e.UserSleeptime).HasColumnName("user_sleeptime");

                entity.Property(e => e.UserStatus)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("user_status");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
