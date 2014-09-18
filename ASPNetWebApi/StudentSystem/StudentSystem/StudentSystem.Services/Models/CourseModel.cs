using StudentSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace StudentSystem.Services.Models
{
    public class CourseModel
    {
        public static Expression<Func<Course, CourseModel>> FromCourse
        {
            get
            {
                return c => new CourseModel
                {
                    CourseId = c.CourseId,
                    Name = c.Name
                };
            }
        }

        public int CourseId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}