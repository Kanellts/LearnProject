using Learn.Core.DataAccess;
using Learn.Core.DataAccess.Models;
using Learn.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Learn.WebApi.Controllers
{
    // localhost:xxxx/api/students
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsService _service;
        public StudentsController(IStudentsService service) {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents() 
        {
            var allStudents = await _service.GetAllStudentsAsync();
            return Ok(allStudents);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> getStudentById(int id) 
        {
            var aStudent = await _service.GetStudentByIdAsync(id);
            if(aStudent is null) 
            {
                return NotFound();
            }
            return Ok(aStudent);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddStudent(AddStudentDTO studentDTO)
        {
            var student = new Student() {
                Name = studentDTO.Name,
                Age = studentDTO.Age
            };
            var newStudent = await _service.CreateStudentAsync(student);
            return CreatedAtAction(nameof(getStudentById), new {id = student.Id}, student);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateStudent(int id, UpdateStudentDTO studentDTO)
        {
            var student = new Student() {
                Id = id,
                Name = studentDTO.Name,
                Age = studentDTO.Age
            };
            await _service.UpdateStudentAsync(student);
            return NoContent();
        }
        
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteStudent(int id) 
        {
            var student = await _service.GetStudentByIdAsync(id);
            if(student is null) {
                return NotFound();
            }
            await _service.DeleteStudentAsync(id);
            return NoContent();
        }
    }
}
