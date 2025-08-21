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
        public IActionResult Add([FromBody] AddStudentRequest request)
        {
            var student = new Student
            {
                Name = request.Name,
                Score = request.Score,
            };
            _context.Students.Add(student);
            _context.SaveChanges();
            
            var response = new AddStudentResponse
            {
                Id = student.Id,
                Name = student.Name,
                Score = student.Score
            };
            return Ok(response);
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var students = _context.Students.ToList();
            var response = students.Select(s => new GetStudentResponse
            {
                Id = s.Id,
                Name = s.Name,
                Score = s.Score
            }).ToList();
            return Ok(response);
        }
        [HttpPost]
        public IActionResult GetById([FromBody] GetStudentByIdRequest request)
        {
            var student = _context.Students.Find(request.Id);
            if (student == null)
            {
                return NotFound();
            }
            
            var response = new GetStudentResponse
            {
                Id = student.Id,
                Name = student.Name,
                Score = student.Score
            };
            return Ok(response);
        }
        [HttpDelete]
        public IActionResult Remove([FromBody] RemoveStudentRequest request)
        {
            var student = _context.Students.Find(request.Id);
            if (student == null)
            {
                return NotFound();
            }
            
            _context.Students.Remove(student);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPut]
        public IActionResult Edit([FromBody] EditStudentRequest request)
        {
            var currentStudent = _context.Students.Find(request.Id);
            if (currentStudent == null)
            {
                return NotFound();
            }
            
            currentStudent.Name = request.Name;
            currentStudent.Score = request.Score;
            _context.SaveChanges();
            
            var response = new GetStudentResponse
            {
                Id = currentStudent.Id,
                Name = currentStudent.Name,
                Score = currentStudent.Score
            };
            return Ok(response);
        }
    }
}
