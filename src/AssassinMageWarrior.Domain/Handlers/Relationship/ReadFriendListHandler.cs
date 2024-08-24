using AssassinMageWarrior.Data.Repository.Relationship.ReadFriendList;
using AssassinMageWarrior.Shareable.Dto.Relationship;
using AssassinMageWarrior.Shareable.Request.Relationship;
using AssassinMageWarrior.Shareable.Response.Relationship;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AssassinMageWarrior.Domain.Handlers.Relationship;

public class ReadFriendListHandler : IRequestHandler<ReadFriendListRequest, ReadFriendListResponse>
{
    private readonly IReadFriendListRepository _repository;
    private readonly ILogger<ReadFriendListHandler> _logger;
    private readonly IMapper _mapper;

    public ReadFriendListHandler(IReadFriendListRepository repository, ILogger<ReadFriendListHandler> logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<ReadFriendListResponse> Handle(ReadFriendListRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando ReadFriendListHandler...");
        var userFriendList = await _repository.GetFriendList(request.Id);

        var response = new List<FriendDto>();

        if (!string.IsNullOrWhiteSpace(userFriendList))
        {
            var friends = userFriendList.Split(";");
            foreach (var item in friends)
            {
                var json = JsonConvert.DeserializeObject<FriendDto>(item);
                var friend = await _repository.GetUsers(json.Id);
                var friendDto = _mapper.Map<FriendDto>(friend);
                response.Add(friendDto!);
            }
        }

        return new ReadFriendListResponse(response.ToArray());
    }
}
