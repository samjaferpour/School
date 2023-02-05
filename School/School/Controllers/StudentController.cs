using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.Contexts;
using School.Dtos;
using School.Entities;

namespace School.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        public StudentController(SchoolDbContext context)
        {
            this._context = context;
        }
        [HttpPost]
        public IActionResult Add([FromBody] AddStudentDto request)
        {
            var student = new Student
            {
                Name = request.Name,
                Score = request.Score,
            };
            _context.Students.Add(student);
            _context.SaveChanges();
            return Ok(student);
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var students = _context.Students.ToList();
            return Ok(students);
        }
        [HttpGet]
        public IActionResult GetById([FromQuery] int id)
        {
            var student = _context.Students.Find(id);
            return Ok(student);
        }
        [HttpDelete]
        public IActionResult Remove([FromQuery] int id)
        {
            var student = _context.Students.Find(id);
            _context.Students.Remove(student);
            _context.SaveChanges();
            return Ok(student);
        }
        [HttpPut]
        public IActionResult Edit([FromBody] EditStudentDto request)
        {
            var currentStudent = _context.Students.Find(request.Id);
            currentStudent.Name = request.Name;
            currentStudent.Score = request.Score;
            _context.SaveChanges();
            return Ok(request);
        }
    }
}
