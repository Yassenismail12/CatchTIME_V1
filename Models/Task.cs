using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TESTT.Models
{
    public enum Difficulty
    {
        Low = 1,
        Medium = 2,
        High = 3
    }

    public enum Priority
    {
        Low = 1,
        Medium = 2,
        High = 3
    }
    public partial class Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskId { get; set; }
        public string? TaskTitle { get; set; }
        [EnumDataType(typeof(Priority))]
        public Priority? TaskPriority { get; set; }
        public string? TaskTag { get; set; }
        public DateTime? TaskDate { get; set; }
        public TimeSpan? TaskDuration { get; set; }
        public TimeSpan? TaskStartTime { get; set; }
        public TimeSpan? TaskEndTime { get; set; }
        public TimeSpan? TaskActualDuration { get; set; }
        [EnumDataType(typeof(Difficulty))]
        public Difficulty? TaskDifficulty { get; set; }
        public bool TaskStatus { get; set; }
        public int? UserId { get; set; }
        public int? ListId { get; set; }
        public int? ProjectId { get; set; }

        //public int? NoOfCompleted { get; set; }
        //public int? NoOfUncompleted { get; set; }
        //public int? TotalTasks { get; set; }

        public virtual List? List { get; set; }
        public virtual Project? Project { get; set; }
        public virtual UserTable? User { get; set; }
    }
}
