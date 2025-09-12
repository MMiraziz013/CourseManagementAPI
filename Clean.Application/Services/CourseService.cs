using Clean.Application.Abstractions;

namespace Clean.Application.Services;

public class CourseService : ICourseService
{
    private readonly ICourseContext _context;

    public CourseService(ICourseContext context)
    {
        _context = context;
    }
    
    public async Task<Response<List<CourseDto>>> GetCoursesAsync()
    {
        var response = await _context.GetCoursesAsync();
        return new Response<List<CourseDto>>(response);
    }
}