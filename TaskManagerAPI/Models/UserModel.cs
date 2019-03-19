using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagerAPI.Models
{
    public class UserModel
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public int? EmployeeId { get; set; }
        public string ProjectName { get; set; }
        public string TaskName { get; set; }
        public int UserStatus { get; set; }
    }
}