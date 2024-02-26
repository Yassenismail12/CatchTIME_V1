// Assuming you have a Task class in your Models namespace
using System.Collections.Generic;

namespace TESTT.Models
{
    public class ListDetailsViewModel
    {
        public int ListId { get; set; }
        public string ListTitle { get; set; }
        public string ListCategory { get; set; }
        public List<Task> Tasks { get; set; }
    }
}
