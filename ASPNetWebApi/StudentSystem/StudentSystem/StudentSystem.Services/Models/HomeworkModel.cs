using StudentSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace StudentSystem.Services.Models
{
    public class HomeworkModel
    {
        public static Expression<Func<Homework, HomeworkModel>> FromHomework
        {
            get
            {
                return h => new HomeworkModel
                {
                    HomeworkId = h.HomeworkId,
                    Content = h.Content
                };
            }
        }

        public int HomeworkId { get; set; }

        [Required]
        public string Content { get; set; }
    }
}