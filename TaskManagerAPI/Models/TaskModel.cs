using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagerAPI.Models
{
    public class TaskModel
    {
        public long taskId { get; set; }
        public string parentTask { get; set; }
        public string task { get; set; }
        public Nullable<System.DateTime> stStartDate { get; set; }
        public Nullable<System.DateTime> stEndDate { get; set; }
        public Nullable<int> priority { get; set; }
        public int taskStatus { get; set; }
    }
}