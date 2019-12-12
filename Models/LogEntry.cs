using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskMVC.Models
{
    public class LogEntry
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public string Message { get; set; }
    }
}
