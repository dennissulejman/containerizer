namespace Containerizer.WebApi;

public class Constants
{
    // Containers
    public const string ContainerStatusCreated = "Created";

    // Docker-specific
    public const string DockerImageLatestTag = "latest";

    // Response Messages
    public const string ResponseMessageContainerContainerCreationStarted =
        "Container creation started";
    public const string ResponseMessageContainerDeletionStarted = "Container deletion started";
    public const string ResponseMessageContainerIdWasCreated = "Container {id} was created";
    public const string ResponseMessageContainerIdWasDeleted = "Container {id} was deleted";
    public const string ResponseMessageContainerIdWasFetched = "Container {id} was fetched";
    public const string ResponseMessageContainerIdWasNotFound = "Container {id} was not found";

    // Routes
    public const string RouteController = "[controller]";
    public const string RouteCreate = "create";
    public const string RouteDeleteFromId = $"delete/{RouteId}";
    public const string RouteStatusFromId = $"status/{RouteId}";
    public const string RouteId = "{id}";
}
