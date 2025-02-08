using System.Threading.Tasks;
using Learn.Core.DataAccess;
using Learn.Core.DataAccess.Models;
using Learn.Core.Services;
using Microsoft.AspNetCore.Http;
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

        // [HttpGet]
        // public IActionResult getStudentById() 
        // {

        // }

        [HttpPost]
        public async Task<IActionResult> AddStudent(AddStudentDTO studentDTO)
        {
            var student = new Student() {
                Name = studentDTO.Name,
                Age = studentDTO.Age
            };
            var newStudent = await _service.CreateStudentAsync(student);
            return Ok(newStudent); 
        }
    }
}
