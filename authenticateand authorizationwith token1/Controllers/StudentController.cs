using authenticateand_authorizationwith_token1.Data;
using authenticateand_authorizationwith_token1.Models;
using authenticateand_authorizationwith_token1.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace authenticateand_authorizationwith_token1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Student
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var students = await _context.Students.ToListAsync();
            return Ok(students);
        }

        // GET: api/Student/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid Student ID");
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound($"No student found with ID {id}");
            }

            return Ok(student);
        }

        // POST: api/Student
        [HttpPost]
        [Authorize (Roles=StaticUserRoles.USER)]
        public async Task<IActionResult> Create([FromBody] StudentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var student = new Student
            {
                StudentID = viewModel.StudentID,
                Name = viewModel.Name,
                Email = viewModel.Email
            };
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudent), new { id = student.StudentID }, student);
        }

        // PUT: api/Student/{id}
        [HttpPut("{id}")]
      // // [Authorize(Roles = StaticUserRoles.USER )]
        public async Task<IActionResult> Edit(string id, [FromBody] StudentViewModel viewModel)
        {
            if (string.IsNullOrEmpty(id) || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound($"No student found with ID {id}");
            }

            student.StudentID = viewModel.StudentID;
            student.Name = viewModel.Name;
            student.Email = viewModel.Email;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Student/{id}
        [HttpDelete("{id}")]
       [Authorize(Roles = StaticUserRoles.USER + "," + StaticUserRoles.ADMIN)]
        public async Task<IActionResult> Delete(string id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound($"No student found with ID {id}");
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return NoContent();
        }


    }
}
