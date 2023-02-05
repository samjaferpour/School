using System.ComponentModel.DataAnnotations;

namespace School.Entities
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set;}
    }
}
