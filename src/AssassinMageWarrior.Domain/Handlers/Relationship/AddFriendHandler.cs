using AssassinMageWarrior.Data.Repository.Relationship.AddFriend;
using AssassinMageWarrior.Shareable.Dto.Relationship;
using AssassinMageWarrior.Shareable.Request.Relationship;
using AssassinMageWarrior.Shareable.Response.Relationship;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AssassinMageWarrior.Domain.Handlers.Relationship;

public class AddFriendHandler : IRequestHandler<AddFriendRequest, AddFriendResponse>
{
    private readonly IAddFriendRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<AddFriendHandler> _logger;

    public AddFriendHandler(IAddFriendRepository repository, IMapper mapper, ILogger<AddFriendHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<AddFriendResponse> Handle(AddFriendRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando AddFriendHandler...");
        await _repository.VerifyList();

        var friend = await _repository.ExistingFriend(request.FriendEmail);
        if (friend is null)
            throw new ApplicationException("Amigo não encontrado!");

        var user = await _repository.ExistingUser(request.UserId);
        if (user is null)
            throw new ApplicationException("Usuário não encontrado!");

        var friendDto = _mapper.Map<FriendDto>(friend);
        var friendList = $"{user.FriendList}{(user.FriendList.Equals(string.Empty) ? string.Empty : ";")}{JsonConvert.SerializeObject(friendDto)}";

        await _repository.UpdateFriendList(user.Id, friendList);

        return new AddFriendResponse("Amigo adicionado com sucesso, verifique em sua lista de amigos!");
    }
}
