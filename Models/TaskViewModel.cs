using System.Collections.Generic;

namespace TESTT.Models
{
    public class TasksViewModel
    {
        public IEnumerable<Task> Tasks { get; set; }
        public IEnumerable<List> Lists { get; set; }
        public Task SelectedTask { get; set; }
    }
}
