using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TESTT.Models
{
    public class List
    {
        public List()
        {
            Projects = new HashSet<Project>();
            Tasks = new HashSet<Task>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ListId { get; set; }
        public string? ListTitle { get; set; }
        public string? ListCategory { get; set; }
        public int? UserId { get; set; }  // Make it nullable

        public virtual UserTable? User { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }

}
