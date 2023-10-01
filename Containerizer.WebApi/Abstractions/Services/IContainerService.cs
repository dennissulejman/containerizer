namespace Containerizer.WebApi.Abstractions.Services;

public interface IContainerService
{
    Task CreateAsync(string imageName);
}
