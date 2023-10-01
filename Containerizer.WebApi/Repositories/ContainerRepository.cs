using Containerizer.WebApi.Abstractions.Repositories;
using Containerizer.WebApi.Models;

namespace Containerizer.WebApi.Repositories;

public class ContainerRepository : IContainerRepository
{
    private readonly Dictionary<string, string> _containers = new();

    public void Add(Container container)
    {
        _containers.Add(container.Id, container.Status);
    }

    public Container Get(string id)
    {
        if (_containers.TryGetValue(id, out string? status))
        {
            return new(id, status);
        }
        else
        {
            throw new KeyNotFoundException();
        }
    }

    public void Update(Container container)
    {
        if (_containers.TryGetValue(container.Id, out string? _))
        {
            _containers[container.Id] = container.Status;
        }
        else
        {
            throw new KeyNotFoundException();
        }
    }

    public void Remove(string id)
    {
        if (_containers.TryGetValue(id, out string? _))
        {
            _containers.Remove(id);
        }
        else
        {
            throw new KeyNotFoundException();
        }
    }
}
