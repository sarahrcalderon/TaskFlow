using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TaskFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProfileController : ControllerBase
{
  [HttpGet]
  public IActionResult GetProfile()
  {
    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
        ?? User.FindFirstValue("sub");

    var name = User.FindFirstValue(ClaimTypes.Name)
        ?? User.FindFirstValue("name");

    var email = User.FindFirstValue(ClaimTypes.Email)
        ?? User.FindFirstValue("email");

    return Ok(new
    {
      userId,
      name,
      email
    });
  }
}