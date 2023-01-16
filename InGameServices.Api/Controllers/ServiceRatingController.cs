using InGameServices.Application.Services.Abstractions;
using InGameServices.Models.ServiceRating.Messages.Request;
using Microsoft.AspNetCore.Mvc;

namespace InGameServices.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ServiceRatingController : ControllerBase
{
  private readonly IServiceRatingService _serviceRatingService;
  
  public ServiceRatingController(IServiceRatingService serviceRatingService)
  {
    _serviceRatingService = serviceRatingService;
  }

  [HttpPost]
  public async Task<ActionResult> Create([FromBody] CreateServiceRatingRequest request)
  {
    try
    {
      var result = await _serviceRatingService.Create(request);
      return Ok(result);
    }
    catch (Exception ex)
    {
      return BadRequest(ex.Message);
    }
  }

  [HttpPut]
  public async Task<ActionResult> Update([FromBody] UpdateServiceRatingRequest request)
  {
    try
    {
      var result = await _serviceRatingService.Update(request);
      return Ok(result);
    }
    catch (Exception ex)
    {
      return BadRequest(ex.Message);
    }
  }

  [HttpDelete]
  public async Task<ActionResult> Delete([FromBody] DeleteServiceRatingRequest request)
  {
    try
    {
      var result = await _serviceRatingService.Delete(request);
      return Ok(result);
    }
    catch (Exception ex)
    {
      return BadRequest(ex.Message);
    }
  }
}