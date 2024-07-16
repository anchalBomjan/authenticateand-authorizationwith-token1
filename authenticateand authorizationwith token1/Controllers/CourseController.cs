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
    public class CourseController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public CourseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Course
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses.ToListAsync();
            return Ok(courses);
        }

        // GET: api/Course/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourse(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid Course ID");
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound($"No course found with ID {id}");
            }

            return Ok(course);
        }

        // POST: api/Course
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CourseViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var course = new Course
            {
                CourseName = viewModel.CourseName,
                CourseDescription = viewModel.CourseDescription,
                Credits = viewModel.Credits
            };
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCourse), new { id = course.CourseID }, course);
        }

        // PUT: api/Course/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] CourseViewModel viewModel)
        {
            if (id <= 0 || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound($"No course found with ID {id}");
            }

            course.CourseName = viewModel.CourseName;
            course.CourseDescription = viewModel.CourseDescription;
            course.Credits = viewModel.Credits;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Course/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound($"No course found with ID {id}");
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
