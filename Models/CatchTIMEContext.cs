﻿using System;
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

        public DbSet<List> Lists { get; set; } = null!;
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<Task> Tasks { get; set; } = null!;
        public DbSet<UserTable> UserTables { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-SQP99UK;Initial Catalog=CatchTime;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            }
            optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<List>(entity =>
            {
                entity.ToTable("List");

                entity.HasKey(e => e.ListId);
                entity.Property(e => e.ListId)
                    .ValueGeneratedOnAdd();


                entity.Property(e => e.ListCategory)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ListCategory");

                entity.Property(e => e.ListTitle)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ListTitle");

                entity.Property(e => e.UserId).HasColumnName("UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Lists)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__List__user_id");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Project");

                entity.HasKey(e => e.ProjectId);
                entity.Property(e => e.ProjectId)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ListId).HasColumnName("ListId");

                entity.Property(e => e.ProjectNoOfCompleted).HasColumnName("ProjectNoOfCompleted");

                entity.Property(e => e.ProjectNoOfTasks).HasColumnName("ProjectNoOfTasks");

                entity.Property(e => e.ProjectTitle)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ProjectTitle");

                entity.Property(e => e.UserId).HasColumnName("UserId");

                entity.HasOne(d => d.List)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.ListId)
                    .HasConstraintName("FK__Project__list_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Project__user_id");
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
                entity.HasKey(e => e.UserId);

                entity.ToTable("User_Table");

                entity.Property(e => e.UserId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("UserId");

                entity.Property(e => e.UserActivities)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("UserActivities");

                entity.Property(e => e.UserAge).HasColumnName("UserAge");

                entity.Property(e => e.UserBirthdate)
                    .HasColumnType("date")
                    .HasColumnName("UserBirthdate");

                entity.Property(e => e.UserCountry)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("UserCountry");

                entity.Property(e => e.UserEmail)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("UserEmail");

                entity.Property(e => e.UserFirstname)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("UserFirstname");

                entity.Property(e => e.UserLastname)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("UserLastname");

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("UserPassword");

                entity.Property(e => e.UserProductivityTime).HasColumnName("UserProductivityTime");

                entity.Property(e => e.UserSleeptime).HasColumnName("UserSleeptime");

                entity.Property(e => e.UserStatus)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("UserStatus");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Username");
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

