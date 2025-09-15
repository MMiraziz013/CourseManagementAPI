using Clean.Application.Abstractions;

namespace Clean.Application.Services;

public class SingletonService : ISingletonService
{
    private Guid _guid;

    public SingletonService()
    {
        _guid = Guid.NewGuid();
    }
    public Guid GetSingleton()
    {
        return _guid;
    }
}