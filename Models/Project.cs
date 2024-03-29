using System;
using System.Collections.Generic;

namespace TESTT.Models
{
    public partial class Project
    {
        public Project()
        {
            Tasks = new HashSet<Task>();
        }

        public int ProjectId { get; set; }
        public string? ProjectTitle { get; set; }
        public int? ProjectNoOfTasks { get; set; }
        public int? ProjectNoOfCompleted { get; set; }
        public int? UserId { get; set; }
        public int? ListId { get; set; }

        public virtual List? List { get; set; }
        public virtual UserTable? User { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }

        public double CompletionPercentage
        {
            get
            {
                if (ProjectNoOfTasks == 0 || ProjectNoOfTasks == null)
                    return 0;

                return ((double)ProjectNoOfCompleted / (double)ProjectNoOfTasks) * 100;
            }
        }
    }
}
