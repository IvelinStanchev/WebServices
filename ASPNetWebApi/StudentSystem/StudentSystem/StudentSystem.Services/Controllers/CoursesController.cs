namespace StudentSystem.Services.Controllers
{
    using StudentSystem.Data;
    using StudentSystem.Models;
    using StudentSystem.Services.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class CoursesController : ApiController
    {
        private IStudentSystemData data;

        public CoursesController()
            : this(new StudentSystemData())
        {
        }

        public CoursesController(IStudentSystemData data)
        {
            this.data = data;
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var courses = this.data.Courses.All()
                .Select(CourseModel.FromCourse);

            return Ok(courses);
        }

        [HttpGet]
        public IHttpActionResult ById(int id)
        {
            var course = this.data.Courses
                .All()
                .Where(c => c.CourseId == id)
                .Select(CourseModel.FromCourse)
                .FirstOrDefault();

            if (course == null)
            {
                return BadRequest("Course does not exist!");
            }

            return Ok(course);
        }

        [HttpPost]
        public IHttpActionResult Create(CourseModel course)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newCourse = new Course
            {
                Name = course.Name
            };

            this.data.Courses.Add(newCourse);
            this.data.SaveChanges();

            course.CourseId = newCourse.CourseId;

            return Ok(course);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, CourseModel course)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingCourse = this.data.Courses.All().First(c => c.CourseId == id);

            if (existingCourse == null)
            {
                return BadRequest("Such course does not exist!");
            }

            existingCourse.Name = course.Name;
            this.data.SaveChanges();

            course.CourseId = id;

            return Ok(course);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var existingCourse = this.data.Courses.All().First(c => c.CourseId == id);

            if (existingCourse == null)
            {
                return BadRequest("Such course does not exist!");
            }

            this.data.Courses.Delete(existingCourse);
            this.data.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult AddStudent(int id, int studentId)
        {
            var course = this.data.Courses.All().FirstOrDefault(c => c.CourseId == id);
            if (course == null)
            {
                return BadRequest("Such course does not exist!");
            }

            var student = this.data.Students.All().FirstOrDefault(s => s.StudentId == studentId);
            if (student == null)
            {
                return BadRequest("Such student does not exist!");
            }

            course.Students.Add(student);
            this.data.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult AddHomework(int id, int homeworkId)
        {
            var course = this.data.Courses.All().FirstOrDefault(c => c.CourseId == id);
            if (course == null)
            {
                return BadRequest("Such course does not exist!");
            }

            var homework = this.data.Homeworks.All().FirstOrDefault(h => h.HomeworkId == homeworkId);
            if (homework == null)
            {
                return BadRequest("Such homework does not exist!");
            }

            course.Homeworks.Add(homework);
            this.data.SaveChanges();

            return Ok();
        }
    }
}
