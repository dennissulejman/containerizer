using System;
using System.Collections.Generic;
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

    [TestMethod]
    public void GetNonExistingContainer_ThrowsKeyNotFoundException()
    {
        Assert.ThrowsException<KeyNotFoundException>(() => _repository.Get("NonExisting"));
    }

    [TestMethod]
    public void UpdateNonExistingContainer_ThrowsKeyNotFoundException()
    {
        Assert.ThrowsException<KeyNotFoundException>(
            () => _repository.Update(new Container("NonExisting", ""))
        );
    }

    [TestMethod]
    public void RemoveNonExistingContainer_ThrowsKeyNotFoundException()
    {
        Assert.ThrowsException<KeyNotFoundException>(() => _repository.Remove("NonExisting"));
    }
}
