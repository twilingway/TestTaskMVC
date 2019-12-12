using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TestTaskMVC.Data;

namespace TestTaskMVC.Models
{
    public static class SeedData
    {
        private static Random random = new Random();
        private static DateTime start = new DateTime(2010, 1, 1);
        private static int range;
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new TestTaskMVCContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<TestTaskMVCContext>>()))
            {
                if (context.User.Any())
                {
                    return;
                }
                await Task.Run(() =>
                {
                    context.User.AddRange(GetSampleUsers(50));
                    context.SaveChanges();
                });
            }
        }

        private static IEnumerable<User> GetSampleUsers(int count)
        {
            for (int i = 0; i < count; i++)
                yield return new User
                {
                    Name = "TestName" + i,
                    Birthday = RandomDay(),
                    IsMale = random.Next(100) <= 50 ? true : false,
                    Request = random.Next(30)
                };
        }

        private static DateTime RandomDay()
        {
            range = (DateTime.Today - start).Days;
            return start.AddDays(random.Next(range));
        }
    }
}
