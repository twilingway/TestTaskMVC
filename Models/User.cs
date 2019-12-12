using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestTaskMVC.Models
{
    public class User
    {
        public int ID { get; set; }
        [RegularExpression(@"^[A-ZА-ЯЁ]+[a-zA-Zа-яА-ЯёЁ0-9""'\s-]*$")]
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        public bool IsMale { get; set; }
        [RegularExpression(@"^[0-9]{1,8}$")]
        public int Request { get; set; }
    }
}
