namespace Clean.Application.Abstractions;

public interface ICourseService
{
    Task<Response<List<CourseDto>>> GetCoursesAsync();
}