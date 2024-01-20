using System;
using System.Collections.Generic;

namespace TESTT.Models
{
    public partial class List
    {
        public List()
        {
            Projects = new HashSet<Project>();
            Tasks = new HashSet<Task>();
        }

        public int ListId { get; set; }
        public string? ListTitle { get; set; }
        public string? ListCategory { get; set; }
        public int? UserId { get; set; }

        public virtual UserTable? User { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
