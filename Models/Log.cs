using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskMVC.Data;

namespace TestTaskMVC.Models
{
    public class Log
    {
        private readonly TestTaskMVCContext _context;

        public Log(TestTaskMVCContext context)
        {
            _context = context;
        }

        public void WriteInfo(string message)
        {
            _context.Log.Add(new LogEntry()
            {
                CreationTime = DateTime.Now,
                Message = message
            });

            _context.SaveChanges();
        }
    }
}
