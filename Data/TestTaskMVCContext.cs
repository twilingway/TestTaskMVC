using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestTaskMVC.Models;

namespace TestTaskMVC.Data
{
    public class TestTaskMVCContext : DbContext
    {
        public TestTaskMVCContext (DbContextOptions<TestTaskMVCContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }
    }
}
