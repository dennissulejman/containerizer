using Containerizer.WebApi.Models;

namespace Containerizer.WebApi.Abstractions.Repositories;

public interface IContainerRepository
{
    void Add(Container container);
    Container Get(string id);
    void Update(Container container);
    void Remove(string id);
}
