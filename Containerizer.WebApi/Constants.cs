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
    public const string ResponseMessageContainerIdWasCreated = "Container {id} was created";

    // Routes
    public const string RouteController = "[controller]";
    public const string RouteCreate = "create";
}
