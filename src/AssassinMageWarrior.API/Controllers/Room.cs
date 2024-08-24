using AssassinMageWarrior.Shareable.Dto.Room;
using AssassinMageWarrior.Shareable.Request.Room;
using AssassinMageWarrior.Shareable.Response.Room;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssassinMageWarrior.API.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class Room : ControllerBase
{
    private readonly IMediator _mediator;

    public Room(IMediator mediator) => _mediator = mediator;


    [HttpGet("/openroom/{id}")]
    [Produces(typeof(OpenRoomResponse))]
    public async Task<IActionResult> OpenRoom(long id)
    {
        var request = new OpenRoomRequest(id);
        var response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpGet("/closeroom/{id}")]
    public async Task<IActionResult> CloseRoom(long id)
    {
        var request = new CloseRoomRequest(id);
        await _mediator.Send(request);

        return Ok();
    }

    [HttpGet("/usersinroom/{id}")]
    [Produces(typeof(UserInRoomResponse))]
    public async Task<IActionResult> GetUsersInRoom(long id)
    {
        var request = new UsersInRoomRequest(id);
        var response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPost("/invite")]
    public async Task<IActionResult> InviteFriend([FromBody] SendInviteDto inviteOptions)
    {
        var request = new InviteToRoomRequest(inviteOptions.UserId, inviteOptions.FriendId);
        await _mediator.Send(request);

        return Ok();
    }

    [HttpGet("/receiveInvite/{id}")]
    [Produces(typeof(ReceiveInviteResponse))]
    public async Task<IActionResult> ReceiveInvite(long id)
    {
        var request = new ReceiveInviteRequest(id);
        var response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPut("/acceptinvite/{id}")]
    public async Task<IActionResult> AcceptInvite(long id)
    {
        var request = new AcceptInviteRequest(id);
        await _mediator.Send(request);

        return Ok();
    }

    [HttpPut("/cancelinvite/{id}")]
    public async Task<IActionResult> CancelInvite(long id)
    {
        var request = new CancelInviteRequest(id);
        await _mediator.Send(request);

        return Ok();
    }
}
