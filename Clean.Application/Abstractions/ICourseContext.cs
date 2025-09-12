namespace Clean.Application.Abstractions;

public interface ICourseContext
{
    Task<List<CourseDto>> GetCoursesAsync();
}