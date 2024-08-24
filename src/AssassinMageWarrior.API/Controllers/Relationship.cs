using AssassinMageWarrior.Shareable.Dto.Relationship;
using AssassinMageWarrior.Shareable.Request.Relationship;
using AssassinMageWarrior.Shareable.Request.Room;
using AssassinMageWarrior.Shareable.Response.Relationship;
using AssassinMageWarrior.Shareable.Response.Room;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssassinMageWarrior.API.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class Relationship : ControllerBase
{
    private readonly IMediator _mediator;

    public Relationship(IMediator mediator) => _mediator = mediator;

    [HttpPut("/addfriend/{id}")]
    [Produces(typeof(AddFriendResponse))]
    public async Task<IActionResult> AddFriend([FromBody] AddFriendDto email, long id)
    {
        var request = new AddFriendRequest(email.Email, id);
        var response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpGet("/getfriends/{id}")]
    [Produces(typeof(ReadFriendListResponse))]
    public async Task<IActionResult> GetFriend(long id)
    {
        var request = new ReadFriendListRequest(id);
        var response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPut("/inactiveuser")]
    public async Task<IActionResult> SetInactiveUser()
    {
        var authHeader = Request.Headers.Authorization.ToString();

        if (authHeader == null || !authHeader.StartsWith("Bearer "))
            return Unauthorized();

        var token = authHeader.Substring("Bearer ".Length).Trim();

        var request = new InactiveUserRequest(token);
        await _mediator.Send(request);

        return Ok();
    }
}
