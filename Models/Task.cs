using System;
using System.Collections.Generic;

namespace TESTT.Models
{
    public partial class Task
    {
        public int TaskId { get; set; }
        public string? TaskTitle { get; set; }
        public int? TaskPriority { get; set; }
        public string? TaskTag { get; set; }
        public DateTime? TaskDate { get; set; }
        public TimeSpan? TaskDuration { get; set; }
        public TimeSpan? TaskStartTime { get; set; }
        public TimeSpan? TaskEndTime { get; set; }
        public TimeSpan? TaskActualDuration { get; set; }
        public int? TaskDifficulty { get; set; }
        public string? TaskStatus { get; set; }
        public int? UserId { get; set; }
        public int? ListId { get; set; }
        public int? ProjectId { get; set; }

        public virtual List? List { get; set; }
        public virtual Project? Project { get; set; }
        public virtual UserTable? User { get; set; }
    }
}
