using StudentSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace StudentSystem.Services.Models
{
    public class StudentModel
    {
        public static Expression<Func<Student, StudentModel>> FromStudent
        {
            get
            {
                return s => new StudentModel
                {
                    StudentId = s.StudentId,
                    Name = s.Name,
                    Number = s.Number
                };
            }
        }

        public int StudentId { get; set; }

        [Required]
        public string Name { get; set; }

        public int Number { get; set; }
    }
}