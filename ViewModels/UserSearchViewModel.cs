using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskMVC.Models
{
    public class UserSearchViewModel
    {
        public PaginatedList<User> Users { get; set; }
        public string IDSort { get; set; }
        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string GenderSort { get; set; }
        public string RequestSort { get; set; }
        public string CurrentSort { get; set; }
        public string CurrentFilter { get; set; }
    }
}
