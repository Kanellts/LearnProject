﻿using System.ComponentModel.DataAnnotations;

namespace Learn.Core.DataAccess.Models
{
    public class Student
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Age { get; set; }

    }
}
