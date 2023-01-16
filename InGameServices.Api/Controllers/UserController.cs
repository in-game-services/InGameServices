using InGameServices.Application.Services.Abstractions;
using InGameServices.Models.User.Messages.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InGameServices.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
  private readonly IUserService _userService;
  
  public UserController(IUserService userService)
  {
    _userService = userService;
  }

  [HttpPost]
  public async Task<ActionResult> Create([FromBody] CreateUserRequest request)
  {
    try
    {
      var result = await _userService.Create(request);
      return Ok(result);
    }
    catch (Exception ex)
    {
      return BadRequest(ex.Message);
    }
  }

  [HttpPut("{id}")]
  [Authorize]
  public async Task<ActionResult> Update([FromBody] UpdateUserRequest request, Guid id)
  {
    try
    {
      var result = await _userService.Update(request, id);
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
      var result = await _userService.GetById(id);
      return Ok(result);
    }
    catch (Exception ex)
    {
      return BadRequest(ex.Message);
    }
  }
}