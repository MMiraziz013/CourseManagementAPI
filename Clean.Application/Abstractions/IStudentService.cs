namespace Clean.Application.Abstractions;

public interface IStudentService
{
    Task<Response<List<StudentDto>>> GetStudentsAsync();
    Task<Response<List<StudentFilterDto>>> SearchStudentsAsync(StudentSearchDto studentSearchDto);

    Task<Response<StudentDto>> GetByIdAsync(int id);
}