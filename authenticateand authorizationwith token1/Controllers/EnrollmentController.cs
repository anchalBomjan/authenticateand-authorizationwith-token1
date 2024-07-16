using authenticateand_authorizationwith_token1.Data;
using authenticateand_authorizationwith_token1.Models;
using authenticateand_authorizationwith_token1.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace authenticateand_authorizationwith_token1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Enrollment
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var enrollments = await _context.Enrollments.Include(e => e.Student).Include(e => e.Course).ToListAsync();
            return Ok(enrollments);
        }

        // GET: api/Enrollment/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEnrollment(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid Enrollment ID");
            }

            var enrollment = await _context.Enrollments.Include(e => e.Student).Include(e => e.Course).FirstOrDefaultAsync(e => e.EnrollmentID == id);
            if (enrollment == null)
            {
                return NotFound($"No enrollment found with ID {id}");
            }

            return Ok(enrollment);
        }

        // POST: api/Enrollment
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EnrollmentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var enrollment = new Enrollment
            {
                StudentID = viewModel.StudentID,
                CourseID = viewModel.CourseID,
                EnrollmentDate = viewModel.EnrollmentDate
            };
            await _context.Enrollments.AddAsync(enrollment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEnrollment), new { id = enrollment.EnrollmentID }, enrollment);
        }

        // PUT: api/Enrollment/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] EnrollmentViewModel viewModel)
        {
            if (id <= 0 || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound($"No enrollment found with ID {id}");
            }

            enrollment.StudentID = viewModel.StudentID;
            enrollment.CourseID = viewModel.CourseID;
            enrollment.EnrollmentDate = viewModel.EnrollmentDate;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Enrollment/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound($"No enrollment found with ID {id}");
            }

            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
