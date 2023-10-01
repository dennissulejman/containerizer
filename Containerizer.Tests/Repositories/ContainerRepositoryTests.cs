using System;
using Containerizer.WebApi.Models;
using Containerizer.WebApi.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Containerizer.Tests.Repositories;

[TestClass]
public class ContainerRepositoryTests
{
    private readonly ContainerRepository _repository;

    public ContainerRepositoryTests()
    {
        _repository = new ContainerRepository();
        _repository.Add(new Container("", ""));
    }

    [TestMethod]
    public void AddDuplicateId_ThrowsArgumentException()
    {
        Assert.ThrowsException<ArgumentException>(() => _repository.Add(new Container("", "")));
    }
    }
