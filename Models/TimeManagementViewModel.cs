using System;
using System.Collections.Generic;
using TESTT.Models;
using Task = TESTT.Models.Task;

namespace TESTT.ViewModels
{
    public class TimeManagementViewModel
    {
        public List<List> AllLists { get; set; }
        public List<Task> AllTasks { get; set; }
        public List<Task> TodayTasks { get; set; }
        public List<Task> TomorrowTasks { get; set; }
        public List<Task> SelectedListTasks { get; set; }
        public int AllListId { get; set; } // ID for the "All" list
        public int TodayListId { get; set; } // ID for the "Today" list
        public int TomorrowListId { get; set; } // ID for the "Tomorrow" list
        public Task SelectedTask { get; set; }
    }
}
