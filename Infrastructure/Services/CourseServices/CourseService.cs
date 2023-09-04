namespace Infrastructure
{
    public class CourseService : ICourseService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IFileService _fileService;

        public CourseService(ApplicationDbContext dbContext,IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        public async Task<Response<AddCourseDto>> AddCourseAsync(AddCourseDto model)
        {
            try
            {
                var course = await _dbContext.Courses.FirstOrDefaultAsync(x => x.Title.ToLower().Trim() == model.Title.ToLower().Trim());
                if (course != null) return new Response<AddCourseDto>(HttpStatusCode.Found, "Data already exists");

                string fileName = string.Empty;
                if (model.Logo != null)
                {
                    fileName = await _fileService.AddFileAsync(FolderType.Image, model.Logo);
                }
                var newCourse = new Course
                {
                    Title = model.Title,
                    CreateDate = DateTimeOffset.UtcNow,
                    Logo = fileName,
                };

                await _dbContext.Courses.AddAsync(newCourse);
                var result = await _dbContext.SaveChangesAsync();

                if (result == 0) return new Response<AddCourseDto>(HttpStatusCode.InternalServerError, "Data not added !");
                return new Response<AddCourseDto>(HttpStatusCode.OK, "Data successfully added !");
            }
            catch (Exception ex)
            {
                return new Response<AddCourseDto>(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public async Task<Response<string>> DeleteCourseAsync(int courseId)
        {
            try
            {
                var course = await _dbContext.Courses.FirstOrDefaultAsync(x => x.Id == courseId);
                if (course == null) return new Response<string>(HttpStatusCode.NotFound, "Data not found !");

                _dbContext.Courses.Remove(course);
                await _fileService.DeleteFileAsync(FolderType.Image, course.Logo);
                var result = await _dbContext.SaveChangesAsync();

                if(result==0) return new Response<string>(HttpStatusCode.InternalServerError, "Course not deleted !");
                return new Response<string>(HttpStatusCode.OK, "Course successfully deleted !");
            }
            catch (Exception ex)
            {
                return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public async Task<PagedResponse<List<GetAllCoursesDto>>> GetAllCoursesAsync(CourseFilter filter)
        {
            var validFilter = new CourseFilter(filter.PageNumber, filter.PageSize);

            var courses = _dbContext.Courses.AsQueryable();

            if (filter.Name != null)
            {
                courses = courses.Where(x => x.Title.ToLower().Contains(filter.Name.ToLower()));
            }

            var coursesDto = await _dbContext.Courses.Select(course => new GetAllCoursesDto
            {
                Id = course.Id,
                Title = course.Title,
                Logo = course.Logo,
            }).OrderBy(x => x.Id).Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync();

            if(coursesDto.Count==0) return new PagedResponse<List<GetAllCoursesDto>>(HttpStatusCode.NoContent, "No content ", coursesDto.Count, validFilter.PageNumber, validFilter.PageSize);
            return new PagedResponse<List<GetAllCoursesDto>>(coursesDto, HttpStatusCode.OK, "All courses ", coursesDto.Count, validFilter.PageNumber, validFilter.PageSize);
        }

        public async Task<Response<GetCourseDto>> GetCourseAsync(int courseId)
        {
            var course = await _dbContext.Courses.FirstOrDefaultAsync(x=>x.Id==courseId);
            if (course == null) return new Response<GetCourseDto>(HttpStatusCode.NotFound, "Course not found !");

            var courseDto =  new GetCourseDto
            {
                Id = course.Id,
                Title = course.Title,
                Logo = course.Logo
            };

            return new Response<GetCourseDto>(HttpStatusCode.OK, "Data found", courseDto);
        }

        public async Task<Response<UpdateCourseDto>> UpdateCourseAsync(UpdateCourseDto model)
        {
            try
            {
                var course = await _dbContext.Courses.FirstOrDefaultAsync(x => x.Id == model.CourseId);
                if (course == null) return new Response<UpdateCourseDto>(HttpStatusCode.NotFound, "Data not found !");

                var fileName = course.Logo??string.Empty;
                if (model.Logo != null)
                {
                    fileName = await _fileService.UpdateFileAsync(FolderType.Image, model.Logo, fileName);

                    course.Logo = fileName;
                }
                course.Title = model.Title;
                course.UpdateDate = DateTimeOffset.UtcNow;
                

                var result = await _dbContext.SaveChangesAsync();

                if (result == 0) return new Response<UpdateCourseDto>(HttpStatusCode.InternalServerError, "Data not updated !");
                return new Response<UpdateCourseDto>(HttpStatusCode.OK, "Data successfully updated !");
            }
            catch (Exception ex)
            {
                return new Response<UpdateCourseDto>(HttpStatusCode.InternalServerError, ex.Message);
            }
            
        }
    }
}
