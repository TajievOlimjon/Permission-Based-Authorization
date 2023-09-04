namespace Infrastructure
{
    public interface ICourseService
    {
        Task<PagedResponse<List<GetAllCoursesDto>>> GetAllCoursesAsync(CourseFilter filter);
        Task<Response<GetCourseDto>> GetCourseAsync(int courseId);
        Task<Response<AddCourseDto>> AddCourseAsync(AddCourseDto model);
        Task<Response<UpdateCourseDto>> UpdateCourseAsync(UpdateCourseDto model);
        Task<Response<string>> DeleteCourseAsync(int courseId);
    }
}
