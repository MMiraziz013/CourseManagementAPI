using Clean.Application.Abstractions;

namespace Clean.Application.Services;

public class ScopedService : IScopedService
{
    private Guid _guid;

    public ScopedService()
    {
        _guid = Guid.NewGuid();
    }
    public Guid GetUnique()
    {
        
        return _guid;
    }
}