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
    public class StudentsController : ApiController
    {
        private IStudentSystemData data;

        public StudentsController()
            : this(new StudentSystemData())
        {
        }

        public StudentsController(IStudentSystemData data)
        {
            this.data = data;
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var students = this.data.Students.All()
                .Select(StudentModel.FromStudent);

            return Ok(students);
        }

        [HttpGet]
        public IHttpActionResult ById(int id)
        {
            var student = this.data.Students
                .All()
                .Where(s => s.StudentId == id)
                .Select(StudentModel.FromStudent)
                .FirstOrDefault();

            if (student == null)
            {
                return BadRequest("Course does not exist!");
            }

            return Ok(student);
        }

        [HttpPost]
        public IHttpActionResult Create(StudentModel student)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newStudent = new Student
            {
                Name = student.Name,
                Number = student.Number
            };

            this.data.Students.Add(newStudent);
            this.data.SaveChanges();

            student.StudentId = newStudent.StudentId;

            return Ok(student);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, StudentModel student)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingStudent = this.data.Students.All().First(s => s.StudentId == id);

            if (existingStudent == null)
            {
                return BadRequest("Such student does not exist!");
            }

            existingStudent.Name = student.Name;
            existingStudent.Number = student.Number;
            this.data.SaveChanges();

            student.StudentId = id;

            return Ok(student);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var existingStudent = this.data.Students.All().First(s => s.StudentId == id);

            if (existingStudent == null)
            {
                return BadRequest("Such student does not exist!");
            }

            this.data.Students.Delete(existingStudent);
            this.data.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult AddCourse(int id, int courseId)
        {
            var student = this.data.Students.All().First(s => s.StudentId == id);
            if (student == null)
            {
                return BadRequest("Such student does not exist!");
            }

            var course = this.data.Courses.All().FirstOrDefault(c => c.CourseId == courseId);
            if (course == null)
            {
                return BadRequest("Such course does not exist!");
            }

            student.Courses.Add(course);
            this.data.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult AddHomework(int id, int homeworkId)
        {
            var student = this.data.Students.All().First(s => s.StudentId == id);
            if (student == null)
            {
                return BadRequest("Such student does not exist!");
            }

            var homework = this.data.Homeworks.All().FirstOrDefault(h => h.HomeworkId == homeworkId);
            if (homework == null)
            {
                return BadRequest("Such homework does not exist!");
            }

            student.Homeworks.Add(homework);
            this.data.SaveChanges();

            return Ok();
        }
    }
}
