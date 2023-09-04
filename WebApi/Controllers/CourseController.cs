namespace WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [HttpGet("GetAllCourses")]
        [PermissionAuthorize(Permissions.Course.View)]
        public async Task<IActionResult> GetAllCoursesAsync([FromQuery]CourseFilter filter)
        {
            var response = await _courseService.GetAllCoursesAsync(filter);

            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("GetCourse")]
        [PermissionAuthorize(Permissions.Course.View)]
        public async Task<IActionResult> GetCourseAsync([FromQuery] int courseId)
        {
            var response = await _courseService.GetCourseAsync(courseId);

            return StatusCode(response.StatusCode, response);
        }
        [HttpPost("AddCourse")]
        [PermissionAuthorize(Permissions.Course.Create)]
        public async Task<IActionResult> AddCourseAsync([FromForm] AddCourseDto model)
        {
            var response = await _courseService.AddCourseAsync(model);

            return StatusCode(response.StatusCode, response);
        }
        [HttpPut("UpdateCourse")]
        [PermissionAuthorize(Permissions.Course.Edit)]
        public async Task<IActionResult> UpdateCourseAsync([FromForm] UpdateCourseDto model)
        {
            var response = await _courseService.UpdateCourseAsync(model);

            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete("DeleteCourse")]
        [PermissionAuthorize(Permissions.Course.Delete)]
        public async Task<IActionResult> DeleteCourseAsync([FromQuery] int courseId)
        {
            var response = await _courseService.DeleteCourseAsync(courseId);

            return StatusCode(response.StatusCode, response);
        }
    }
}
