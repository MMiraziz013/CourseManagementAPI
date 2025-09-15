using Clean.Application.Abstractions;

namespace Clean.Application.Services;

public class TransientService : ITransientService
{
    private readonly Guid _guid;

    public TransientService()
    {
        _guid = Guid.NewGuid();
    }

    public Guid GetTransient()
    {
        return _guid;
    }
}