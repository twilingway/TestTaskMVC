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
        private static Random _random = new Random();
        private static DateTime _start = new DateTime(2010, 1, 1);
        private static int _range;
        private static int _count = 5000;
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
                    context.User.AddRange(GetSampleUsers(_count));
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
                    IsMale = _random.Next(100) <= 50 ? true : false,
                    Request = _random.Next(30)
                };
        }

        private static DateTime RandomDay()
        {
            _range = (DateTime.Today - _start).Days;
            return _start.AddDays(_random.Next(_range));
        }
    }
}
