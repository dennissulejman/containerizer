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

    public async Task DeleteAsync(string id)
    {
        await _containerChannel.Writer.WriteAsync(async () =>
        {
            try
            {
                using DockerClient dockerClient = GetDockerClient();
                await dockerClient.Containers.RemoveContainerAsync(
                    id,
                    new Docker.DotNet.Models.ContainerRemoveParameters()
                );

                _repository.Remove(id);
                _logger.LogInformation(Constants.ResponseMessageContainerIdWasDeleted, id);
            }
            catch (KeyNotFoundException)
            {
                _logger.LogError(Constants.ResponseMessageContainerIdWasNotFound, id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        });
    }

    public Container Get(string id)
    {
        try
        {
            Container container = _repository.Get(id);
            _logger.LogInformation(Constants.ResponseMessageContainerIdWasFetched, id);
            return container;
        }
        catch (KeyNotFoundException)
        {
            _logger.LogError(Constants.ResponseMessageContainerIdWasNotFound, id);
            return new Container("", $"Container {id} was not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return new Container("", ex.Message);
        }
    }

    public async Task StartAsync(string id)
    {
        await _containerChannel.Writer.WriteAsync(async () =>
        {
            try
            {
                using DockerClient dockerClient = GetDockerClient();
                await dockerClient.Containers.StartContainerAsync(
                    id,
                    new Docker.DotNet.Models.ContainerStartParameters()
                );

                _repository.Update(new(id, Constants.ContainerStatusRunning));
                _logger.LogInformation(Constants.ResponseMessageContainerIdWasStarted, id);
            }
            catch (KeyNotFoundException)
            {
                _logger.LogError(Constants.ResponseMessageContainerIdWasNotFound, id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        });
    }

    public async Task StopAsync(string id)
    {
        await _containerChannel.Writer.WriteAsync(async () =>
        {
            try
            {
                using DockerClient dockerClient = GetDockerClient();
                await dockerClient.Containers.StopContainerAsync(
                    id,
                    new Docker.DotNet.Models.ContainerStopParameters()
                );

                _repository.Update(new(id, Constants.ContainerStatusStopped));
                _logger.LogInformation(Constants.ResponseMessageContainerIdWasStopped, id);
            }
            catch (KeyNotFoundException)
            {
                _logger.LogError(Constants.ResponseMessageContainerIdWasNotFound, id);
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
