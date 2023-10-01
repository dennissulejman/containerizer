using System;
using System.Threading.Channels;
using Containerizer.WebApi.Abstractions.Repositories;
using Containerizer.WebApi.Models;
using Containerizer.WebApi.Repositories;
using Containerizer.WebApi.Services;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Containerizer.Tests.Services;

[TestClass]
public class ContainerServiceTests
{
    private readonly ContainerService _containerService;

    public ContainerServiceTests()
    {
        IContainerRepository repository = new ContainerRepository();

        repository.Add(new Container("", ""));

        _containerService = new ContainerService(
            Channel.CreateUnbounded<Action>(),
            repository,
            LoggerFactory.Create(log => log.AddConsole()).CreateLogger<ContainerService>()
        );
    }

    [TestMethod]
    public void Get_ReturnsMatchingContainer()
    {
        string id = "";
        Container expected = new(id, "");

        Container actual = _containerService.Get(id);

        Assert.AreEqual(expected, actual);
    }
}
