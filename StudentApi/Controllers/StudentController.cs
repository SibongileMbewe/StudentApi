using Microsoft.AspNetCore.Mvc;
using StudentApi.Models;
using StudentApi.Repositories;

namespace StudentApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _repository;

        public StudentController(IStudentRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retrieves all students.
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            var students = _repository.GetAll();
            return Ok(students);
        }

        /// <summary>
        /// Retrieves a student by ID.
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var student = _repository.GetById(id);
            if (student == null)
                return NotFound(new { Message = $"Student with ID {id} was not found." });

            return Ok(student);
        }

        /// <summary>
        /// Adds a new student.
        /// </summary>
        [HttpPost]
        public IActionResult Add([FromBody] Student student)
        {
            if (!ModelState.IsValid)
                return ValidationProblemDetails();

            _repository.Add(student);

            return CreatedAtAction(
                nameof(GetById),
                new { id = student.Id },
                student
            );
        }

        /// <summary>
        /// Deletes a student by ID.
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var student = _repository.GetById(id);
            if (student == null)
                return NotFound(new { Message = $"Student with ID {id} was not found." });

            _repository.Delete(id);
            return NoContent();
        }

        /// <summary>
        /// Updates an existing student.
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Student student)
        {
            if (!ModelState.IsValid)
                return ValidationProblemDetails();

            var existing = _repository.GetById(id);
            if (existing == null)
                return NotFound(new { Message = $"Student with ID {id} not found." });

            student.Id = id; 
            _repository.Update(student);
            return Ok(new { Message = $"Student with ID {id} updated successfully." });
        }

        /// <summary>
        /// Downloads the student data as an XML file.
        /// </summary>
        /// <returns></returns>
        [HttpGet("download-xml")]
        public IActionResult DownloadXml()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "students.xml");
            if (!System.IO.File.Exists(path))
                return NotFound(new { Message = "Student data not found." });

            var fileBytes = System.IO.File.ReadAllBytes(path);
            return File(fileBytes, "application/xml", "students.xml");
        }


        /// <summary>
        /// Handles validation errors and returns a detailed error response.
        /// </summary>
        /// <returns></returns>
        private IActionResult ValidationProblemDetails()
        {
            var errors = ModelState
                .Where(ms => ms.Value.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );

            return BadRequest(new
            {
                Message = "Validation failed. Please fix the following errors.",
                Errors = errors
            });
        }
    }
}
