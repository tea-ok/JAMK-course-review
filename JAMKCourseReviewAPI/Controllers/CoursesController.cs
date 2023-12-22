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

        // GET: /api/courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeacherCourse>>> GetTeacherCourses()
        {
            return await _courseService.GetTeacherCourses();
        }
    }
}