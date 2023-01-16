using InGameServices.Application.Services.Abstractions;
using InGameServices.Models.Service.Messages.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InGameServices.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ServiceController : ControllerBase
{
  private readonly IServiceService _serviceService;
  
  public ServiceController(IServiceService serviceService)
  {
    _serviceService = serviceService;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult> Create([FromBody] CreateServiceRequest request)
  {
    try
    {
      var result = await _serviceService.Create(request);
      return Ok(result);
    }
    catch (Exception ex)
    {
      return BadRequest(ex.Message);
    }
  }

  [HttpPut("{id}")]
  [Authorize]
  public async Task<ActionResult> Update([FromBody] UpdateServiceRequest request, Guid id)
  {
    try
    {
      var result = await _serviceService.Update(request, id);
      return Ok(result);
    }
    catch (Exception ex)
    {
      return BadRequest(ex.Message);
    }
  }

  [HttpDelete("{id}")]
  [Authorize]
  public async Task<ActionResult> Delete(Guid id)
  {
    try
    {
      var result = await _serviceService.Delete(id);
      return Ok(result);
    }
    catch (Exception ex)
    {
      return BadRequest(ex.Message);
    }
  }

  [HttpGet]
  [Authorize]
  public async Task<ActionResult> Get()
  {
    try
    {
      var result = await _serviceService.GetAll();
      return Ok(result);
    }
    catch (Exception ex)
    {
      return BadRequest(ex.Message);
    }
  }

  [HttpGet("{id}")]
  [Authorize]
  public async Task<ActionResult> Get(Guid id)
  {
    try
    {
      var userId = new Guid(User.Claims.Where(x => x.Type == "Id").FirstOrDefault().Value);
      var result = await _serviceService.GetById(id, userId);
      return Ok(result);
    }
    catch (Exception ex)
    {
      return BadRequest(ex.Message);
    }
  }
}