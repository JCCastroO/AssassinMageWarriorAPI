using AssassinMageWarrior.Shareable.Dto.Room;

namespace AssassinMageWarrior.Shareable.Response.Room;

public record ReceiveInviteResponse(bool NewInvite, InviteDto? Invite);
