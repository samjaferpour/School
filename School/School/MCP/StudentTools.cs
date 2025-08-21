using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelContextProtocol.Server;
using School.Contexts;
using School.Dtos;
using School.Entities;
using System.ComponentModel;

namespace School.MCP
{
    [McpServerToolType]
    public class StudentTools
    {
        private readonly SchoolDbContext _schoolDbContext;

        public StudentTools(SchoolDbContext schoolDbContext)
        {
            _schoolDbContext = schoolDbContext;
        }

        [McpServerTool]
        [Description("Adds a new student")]
        public AddStudentResponse AddStudent([Description("Gets a student parameters as a JSON")] AddStudentRequest request)
        {
            var student = new Student
            {
                Name = request.Name,
                Score = request.Score,
            };
            _schoolDbContext.Students.Add(student);
            _schoolDbContext.SaveChanges();

            var response = new AddStudentResponse
            {
                Id = student.Id,
                Name = student.Name,
                Score = student.Score
            };

            return response;
        }


        [McpServerTool]
        [Description("Gets all students")]
        public List<GetStudentResponse> GetAllStudents()
        {
            var students = _schoolDbContext.Students.ToList();
            var response = students.Select(s => new GetStudentResponse
            {
                Id = s.Id,
                Name = s.Name,
                Score = s.Score
            }).ToList();

            return response;
        }



        [McpServerTool]
        [Description("Gets a student by ID")]
        public GetStudentResponse GetStudentById([Description("Gets a student Id as a parameter as a JSON")] GetStudentByIdRequest id)
        {
            var student = _schoolDbContext.Students.Find(id);
            if (student == null)
            {
                return null;
            }

            var response = new GetStudentResponse
            {
                Id = student.Id,
                Name = student.Name,
                Score = student.Score
            };
            return response;
        }



        [McpServerTool]
        [Description("Removes a student")]
        public void RemoveStudent([Description("Get a user Id as a parameter in a JSON")] RemoveStudentRequest request)
        {
            var student = _schoolDbContext.Students.Find(request.Id);


            _schoolDbContext.Students.Remove(student);
            _schoolDbContext.SaveChanges();
        }


        [McpServerTool]
        [Description("Edits a student")]
        public void Edit([Description("Gets student parameters as a JSON")] EditStudentRequest request)
        {
            var currentStudent = _schoolDbContext.Students.Find(request.Id);

            currentStudent.Name = request.Name;
            currentStudent.Score = request.Score;
            _schoolDbContext.SaveChanges();

        }


    }
}
