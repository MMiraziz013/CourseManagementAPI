namespace Clean.Application.Abstractions;

public interface IScopedService
{
    Guid GetUnique();
}