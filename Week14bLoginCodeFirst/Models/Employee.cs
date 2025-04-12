using System.ComponentModel.DataAnnotations;

namespace Week14bLoginCodeFirst.Models
{
    public class Employee
    {
        [Key]     // to make id primary key
        public int Id { get; set; }   // properties  - auto type

        //[Required(ErrorMessage = "Please enter the Email address")]
        //[MaxLength(3, ErrorMessage = "Please enter the Email address")]


        [EmailAddress(ErrorMessage = "Please enter a valid Email address")]
        public required string Email { get; set; }
        [Required(ErrorMessage = "Please enter the Password")]
        public required string Password { get; set; }
        //[Required(ErrorMessage = "Please enter the First Name")]

        //[MaxLength(3,ErrorMessage = "Please enter the FirstName")]
        public string? FirstName { get; set; }


        public string? LastName { get; set; }

        public string? Address { get; set; }


    }
}
