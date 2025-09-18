namespace Clean.Application.Abstractions;

public interface IStudentProfileContext
{
    public Task<int> CreateStudentProfileAsync(AddStudentProfileDto addStudentProfileDto, string filename);

    public Task<GetStudentProfileDto?> GetStudentProfileById(int id);

    public Task<bool> UpdateStudentProfileAsync(UpdateStudentProfileDto updateStudentProfileDto);

}