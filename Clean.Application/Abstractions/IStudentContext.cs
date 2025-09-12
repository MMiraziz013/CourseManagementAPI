namespace Clean.Application.Abstractions;

public interface IStudentContext
{
    Task<List<StudentDto>> GetStudentsAsync();
    Task<List<StudentFilterDto>> SearchStudentsAsync(StudentSearchDto studentSearchDto);
}