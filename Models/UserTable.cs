using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;


namespace TESTT.Models
{
    public partial class UserTable

    {
        public UserTable()
        {
            Lists = new HashSet<List>();
            Projects = new HashSet<Project>();
            Tasks = new HashSet<Task>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPassword { get; set; }
        public DateTime? UserBirthdate { get; set; }
        public string? UserFirstname { get; set; }
        public string? UserLastname { get; set; }
        public string? UserCountry { get; set; }
        public int? UserAge { get; set; }
        public string? UserActivities { get; set; }
        public string? UserStatus { get; set; }
        public TimeSpan? UserSleeptime { get; set; }
        public TimeSpan? UserProductivityTime { get; set; }

        public virtual ICollection<List> Lists { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
