using Containerizer.WebApi.Models;

namespace Containerizer.WebApi.Abstractions.Services;

public interface IContainerService
{
    Task CreateAsync(string imageName);
    Task DeleteAsync(string id);
    Container Get(string id);
    Task StartAsync(string id);
    Task StopAsync(string id);
}
