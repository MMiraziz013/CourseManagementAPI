namespace Clean.Application.Abstractions;

public interface IStudentProfileService
{
    public Task<Response<GetStudentProfileDto>> CreateStudentProfile(AddStudentProfileDto addStudentProfileDto,
        string? filename);

    public Task<Response<GetStudentProfileDto>> UpdateStudentProfile(AddStudentProfileDto updateStudentProfileDto);

}