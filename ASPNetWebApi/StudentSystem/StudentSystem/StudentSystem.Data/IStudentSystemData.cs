namespace StudentSystem.Data
{
    using StudentSystem.Data.Repositories;
    using StudentSystem.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IStudentSystemData
    {
        IRepository<Homework> Homeworks { get; }

        IRepository<Course> Courses { get; }

        IRepository<Student> Students { get; }

        void SaveChanges();
    }
}
