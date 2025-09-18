using System.Net;
using Clean.Application.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Clean.Application.Services;

public class StudentProfileService : IStudentProfileService
{
    private readonly IStudentProfileContext _profileContext;
    private readonly IAppEnvironment _environment;

    public StudentProfileService(IStudentProfileContext profileContext, IAppEnvironment environment)
    {
        _profileContext = profileContext;
        _environment = environment;
    }

    public async Task<Response<GetStudentProfileDto>> CreateStudentProfile(AddStudentProfileDto addStudentProfileDto, string? filename)
    {
        var id = await _profileContext.CreateStudentProfileAsync(addStudentProfileDto, filename);

        return new Response<GetStudentProfileDto>(new GetStudentProfileDto
        {
            Id = id,
            FullName = addStudentProfileDto.FullName,
            Phone = addStudentProfileDto.Phone,
            ProfilePicture = filename != null ? GenerateFileLink(filename) : string.Empty
        });
    }

    private string GenerateFileLink(string filename)
    {
        var path = Path.Combine("http://localhost:5165", "images", filename);
        return path;    
    }
    
    public async Task<Response<GetStudentProfileDto>> UpdateStudentProfile(AddStudentProfileDto updateStudentProfileDto)
    {
        var existingProfile = await _profileContext.GetStudentProfileById(updateStudentProfileDto.Id);
        if (existingProfile == null)
            return new Response<GetStudentProfileDto>( statusCode:HttpStatusCode.NotFound, "Profile not found");

        string? newFilename = null;

        if (updateStudentProfileDto.ProfilePicture != null)
        {
            if (!string.IsNullOrEmpty(existingProfile.ProfilePicture))
            {
                var oldPath = Path.Combine(_environment.WebRootPath, "images", existingProfile.ProfilePicture);
                if (File.Exists(oldPath)) File.Delete(oldPath);
            }
            newFilename = Guid.NewGuid() + Path.GetExtension(updateStudentProfileDto.ProfilePicture.FileName);
            var newPath = Path.Combine(_environment.WebRootPath, "images", newFilename);
            using var stream = new FileStream(newPath, FileMode.Create);
            await updateStudentProfileDto.ProfilePicture.CopyToAsync(stream);
        }
        var toUpdate = new UpdateStudentProfileDto
        {
            Id = updateStudentProfileDto.Id, FullName = updateStudentProfileDto.FullName,
            Phone = updateStudentProfileDto.Phone, ProfilePicture = newFilename ?? existingProfile.ProfilePicture
        };
        

        await _profileContext.UpdateStudentProfileAsync(toUpdate);

        return new Response<GetStudentProfileDto>(new GetStudentProfileDto
        {
            Id = updateStudentProfileDto.Id,
            FullName = updateStudentProfileDto.FullName,
            Phone = updateStudentProfileDto.Phone,
            ProfilePicture = newFilename != null ? GenerateFileLink(newFilename) : existingProfile.ProfilePicture
        });
    }
}
