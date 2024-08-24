using AssassinMageWarrior.Shareable.Dto.Auth;
using AssassinMageWarrior.Shareable.Request.Auth;
using AssassinMageWarrior.Shareable.Response.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssassinMageWarrior.API.Controllers;

[ApiController]
[Route("[controller]")]
public class Auth : ControllerBase
{
    private readonly IMediator _mediator;

    public Auth(IMediator mediator) => _mediator = mediator;

    [HttpPost("/register")]
    [Produces(typeof(RegisterResponse))]
    public async Task<IActionResult> Register([FromBody] RegisterDto user)
    {
        var request = new RegisterRequest(user);
        var response = await _mediator.Send(request);

        return Created("", response);
    }

    [HttpPost("/login")]
    [Produces(typeof(LoginResponse))]
    public async Task<IActionResult> Login([FromBody] LoginDto user)
    {
        var request = new LoginRequest(user);
        var response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpGet("/logged")]
    [Authorize]
    [Produces(typeof(LoggedResponse))]
    public async Task<IActionResult> GetLoggedUser()
    {
        var authHeader = Request.Headers.Authorization.ToString();

        if (authHeader == null || !authHeader.StartsWith("Bearer "))
            return Unauthorized();

        var token = authHeader.Substring("Bearer ".Length).Trim();
        var request = new LoggedRequest(token);
        var response = await _mediator.Send(request);

        return Ok(response);
    }
}
