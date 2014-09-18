using StudentSystem.Data;
using StudentSystem.Models;
using StudentSystem.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StudentSystem.Services.Controllers
{
    public class HomeworksController : ApiController
    {
        private IStudentSystemData data;

        public HomeworksController()
            : this(new StudentSystemData())
        {
        }

        public HomeworksController(IStudentSystemData data)
        {
            this.data = data;
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var homeworks = this.data.Homeworks.All()
                .Select(HomeworkModel.FromHomework);

            return Ok(homeworks);
        }

        [HttpGet]
        public IHttpActionResult ById(int id)
        {
            var homework = this.data.Homeworks
                .All()
                .Where(h => h.HomeworkId == id)
                .Select(HomeworkModel.FromHomework)
                .FirstOrDefault();

            if (homework == null)
            {
                return BadRequest("Homework does not exist!");
            }

            return Ok(homework);
        }

        [HttpPost]
        public IHttpActionResult Create(HomeworkModel homework)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newHomework = new Homework
            {
                Content = homework.Content
            };

            this.data.Homeworks.Add(newHomework);
            this.data.SaveChanges();

            homework.HomeworkId = newHomework.HomeworkId;

            return Ok(homework);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, HomeworkModel homework)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingHomework = this.data.Homeworks.All().First(h => h.HomeworkId == id);

            if (existingHomework == null)
            {
                return BadRequest("Such homework does not exist!");
            }

            existingHomework.Content = homework.Content;
            this.data.SaveChanges();

            homework.HomeworkId = id;

            return Ok(homework);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var existingHomework = this.data.Homeworks.All().First(h => h.HomeworkId == id);
            if (existingHomework == null)
            {
                return BadRequest("Such homework does not exist!");
            }

            this.data.Homeworks.Delete(existingHomework);
            this.data.SaveChanges();

            return Ok();
        }

        //[HttpPost]
        //public IHttpActionResult AddStudent(int id, int studentId)
        //{
        //    var homework = this.data.Homeworks.All().FirstOrDefault(h => h.HomeworkId == id);
        //    if (homework == null)
        //    {
        //        return BadRequest("Such student does not exist!");
        //    }

        //    var student = this.data.Students.All().FirstOrDefault(s => s.StudentId == studentId);
        //    if (student == null)
        //    {
        //        return BadRequest("Such student does not exist!");
        //    }

        //    homework.StudentId = student.StudentId;
        //    this.data.SaveChanges();

        //    return Ok();
        //}

        //[HttpPost]
        //public IHttpActionResult AddCourse(int id, int courseId)
        //{
        //    var homework = this.data.Homeworks.All().FirstOrDefault(h => h.HomeworkId == id);
        //    if (homework == null)
        //    {
        //        return BadRequest("Such student does not exist!");
        //    }

        //    var course = this.data.Courses.All().FirstOrDefault(c => c.CourseId == courseId);
        //    if (course == null)
        //    {
        //        return BadRequest("Such course does not exist!");
        //    }

        //    homework.CourseId = course.CourseId;
        //    this.data.SaveChanges();

        //    return Ok();
        //}
    }
}
