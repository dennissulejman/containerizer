using Containerizer.WebApi.Models;

namespace Containerizer.WebApi.Abstractions.Repositories;

public interface IContainerRepository
{
    void Add(Container container);
    Container Get(string id);
    void Remove(string id);
}
