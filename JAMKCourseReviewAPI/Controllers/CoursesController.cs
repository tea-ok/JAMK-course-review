using JAMKCourseReviewAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JAMKCourseReviewAPI.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly CourseService _courseService;

        public CoursesController(CourseService courseService)
        {
            _courseService = courseService;
        }

        // GET: /api/courses?courseCode={courseCode}
        [HttpGet("course")]
        public async Task<ActionResult<Course>> GetCourseByCode([FromQuery] string courseCode)
        {
            var course = await _courseService.GetCourseByCode(courseCode);

            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        // GET: /api/courses
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetTeacherCourses()
        {
            var result = await _courseService.GetTeacherCourses();
            return Ok(result);
        }
    }
}