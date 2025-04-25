using System.ComponentModel.DataAnnotations;

namespace StudentApi.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the full name.")]
        [StringLength(100, ErrorMessage = "Full name can't exceed 100 characters.")]
        [RegularExpression(@"^[^\d]+$", ErrorMessage = "Full name cannot contain numbers.")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please provide the age.")]
        [Range(5, 120, ErrorMessage = "Age must be between 5 and 120.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Please specify the grade.")]
        [StringLength(10, ErrorMessage = "Grade must be up to 10 characters.")]
        [RegularExpression(@"^\d{1,2}[A-F][\+\-]?$", ErrorMessage = "Grade must be in the format like 10A, 9B+, or 8C-.")]
        public string Grade { get; set; } = string.Empty;
    }
}
