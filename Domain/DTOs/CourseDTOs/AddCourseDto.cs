namespace Domain
{
    public class AddCourseDto : BaseCourseDto
    {
        public IFormFile? Logo { get; set; } = null;
    }
}
