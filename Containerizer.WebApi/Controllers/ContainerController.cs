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
}
