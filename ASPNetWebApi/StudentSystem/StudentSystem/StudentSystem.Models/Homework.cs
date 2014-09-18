namespace StudentSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Homework
    {
        public int HomeworkId { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime? TimeSent { get; set; }

        public int? StudentId { get; set; }

        public virtual Student Student { get; set; }

        public int? CourseId { get; set; }

        public virtual Course Course { get; set; }
    }
}
