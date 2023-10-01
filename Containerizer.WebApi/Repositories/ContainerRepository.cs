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
}
