using AssassinMageWarrior.Data.Repository.Room.ReceiveInvite;
using AssassinMageWarrior.Shareable.Dto.Room;
using AssassinMageWarrior.Shareable.Request.Room;
using AssassinMageWarrior.Shareable.Response.Room;
using AutoMapper;
using Castle.Core.Logging;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AssassinMageWarrior.Domain.Handlers.Room;

public class ReceiveInviteHandler : IRequestHandler<ReceiveInviteRequest, ReceiveInviteResponse>
{
    private readonly ILogger<ReceiveInviteHandler> _logger;
    private readonly IReceiveInviteRepository _repository;
    private readonly IMapper _mapper;

    public ReceiveInviteHandler(ILogger<ReceiveInviteHandler> logger, IReceiveInviteRepository repository, IMapper mapper)
    {
        _logger = logger;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ReceiveInviteResponse> Handle(ReceiveInviteRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando ReceiveInviteHandler...");
        var invite = await _repository.GetInvite(request.Id);
        if (invite is null)
            return new ReceiveInviteResponse(false, null);

        var inviteDto = _mapper.Map<InviteDto>(invite);
        return new ReceiveInviteResponse(true, inviteDto);

    }
}
