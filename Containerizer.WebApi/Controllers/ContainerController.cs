using Containerizer.WebApi.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace Containerizer.WebApi.Controllers;

[ApiController]
[Route(Constants.RouteController)]
public class ContainerController : ControllerBase
{
    private readonly IContainerService _containerService;

    public ContainerController(IContainerService containerService)
    {
        _containerService = containerService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Route(Constants.RouteCreate)]
    public async Task<IActionResult> Create([FromBody] string imageName)
    {
        await _containerService.CreateAsync(imageName);
        return Ok(Constants.ResponseMessageContainerContainerCreationStarted);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Route(Constants.RouteDeleteFromId)]
    public async Task<IActionResult> Delete([FromRoute] string id)
    {
        await _containerService.DeleteAsync(id);
        return Ok(Constants.ResponseMessageContainerDeletionStarted);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Route(Constants.RouteStatusFromId)]
    public IActionResult Status([FromRoute] string id)
    {
        Models.Container container = _containerService.Get(id);
        return Ok(container.Status);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Route(Constants.RouteStartFromId)]
    public async Task<IActionResult> Start([FromRoute] string id)
    {
        await _containerService.StartAsync(id);
        return Ok(Constants.ResponseMessageContainerStartRequested);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Route(Constants.RouteStopFromId)]
    public async Task<IActionResult> Stop([FromRoute] string id)
    {
        await _containerService.StopAsync(id);
        return Ok(Constants.ResponseMessageContainerStopRequested);
    }
}
