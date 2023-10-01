using Containerizer.WebApi.Models;

namespace Containerizer.WebApi.Abstractions.Repositories;

public interface IContainerRepository
{
    void Add(Container container);
}
