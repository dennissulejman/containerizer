using System.Threading.Channels;
using Containerizer.WebApi.Abstractions.Repositories;
using Containerizer.WebApi.Abstractions.Services;
using Containerizer.WebApi.Models;
using Docker.DotNet;

namespace Containerizer.WebApi.Services;

public class ContainerService : IContainerService
{
    private readonly Channel<Action> _containerChannel;
    private readonly IContainerRepository _repository;
    private readonly ILogger<ContainerService> _logger;

    public ContainerService(
        Channel<Action> containerChannel,
        IContainerRepository repository,
        ILogger<ContainerService> logger
    )
    {
        _containerChannel = containerChannel;
        _repository = repository;
        _logger = logger;
    }

    public async Task CreateAsync(string imageName)
    {
        await _containerChannel.Writer.WriteAsync(async () =>
        {
            try
            {
                using DockerClient dockerClient = GetDockerClient();
                await dockerClient.Images.CreateImageAsync(
                    new Docker.DotNet.Models.ImagesCreateParameters
                    {
                        FromImage = imageName,
                        Tag = Constants.DockerImageLatestTag,
                    },
                    null,
                    new Progress<Docker.DotNet.Models.JSONMessage>()
                );

                Docker.DotNet.Models.CreateContainerResponse response =
                    await dockerClient.Containers.CreateContainerAsync(
                        new Docker.DotNet.Models.CreateContainerParameters { Image = imageName }
                    );

                Container container = new(response.ID, Constants.ContainerStatusCreated);
                _repository.Add(container);
                _logger.LogInformation(
                    Constants.ResponseMessageContainerIdWasCreated,
                    container.Id
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        });
    }
    private static DockerClient GetDockerClient()
    {
        return new DockerClientConfiguration().CreateClient();
    }
}
