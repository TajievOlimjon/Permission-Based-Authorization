namespace Domain
{
    public class UpdateCourseDto : BaseCourseDto
    {
        public int CourseId { get; set; }
        public IFormFile? Logo { get; set; } = null;
    }
}
