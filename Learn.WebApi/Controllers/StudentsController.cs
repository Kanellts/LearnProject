using System.Threading.Tasks;
using Learn.Core.DataAccess;
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

        
    }
}
