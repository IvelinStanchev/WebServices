namespace StudentSystem.Data
{
    using StudentSystem.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IStudentSystemDbContext
    {
        IDbSet<Homework> Homeworks { get; set; }

        IDbSet<Course> Courses { get; set; }

        IDbSet<Student> Students { get; set; }

        void SaveChanges();

        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
