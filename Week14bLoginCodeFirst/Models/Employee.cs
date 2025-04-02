using System.ComponentModel.DataAnnotations;

namespace Week14bLoginCodeFirst.Models
{
    public class Employee
    {
        [Key]     // to make id primary key
        public int Id { get; set; }   // properties  - auto type

        public string Email { get; set; }

        public string Password { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Address { get; set; }
    }
}
