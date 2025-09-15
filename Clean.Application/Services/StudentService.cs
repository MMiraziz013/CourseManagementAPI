using Clean.Application.Abstractions;

namespace Clean.Application.Services;

public class StudentService : IStudentService
{
    private readonly IStudentContext _studentContext;

    public StudentService(IStudentContext studentContext)
    {
        _studentContext = studentContext;
    }
    
    public async Task<Response<List<StudentDto>>> GetStudentsAsync()
    {
        var response = await _studentContext.GetStudentsAsync();

        return new Response<List<StudentDto>>(response);

    }

    public async Task<Response<List<StudentFilterDto>>> SearchStudentsAsync(StudentSearchDto studentSearchDto)
    {
        var response = await _studentContext.SearchStudentsAsync(studentSearchDto);
        return new Response<List<StudentFilterDto>>(response);
    }

    public async Task<Response<StudentDto>> GetByIdAsync(int id)
    {
        var response = await _studentContext.GetByIdAsync(id);
        return new Response<StudentDto>(response);
    }
}