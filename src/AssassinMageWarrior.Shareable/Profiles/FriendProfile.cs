using AssassinMageWarrior.Data.Entities;
using AssassinMageWarrior.Shareable.Dto.Relationship;
using AutoMapper;

namespace AssassinMageWarrior.Shareable.Profiles;

internal class FriendProfile : Profile
{
    public FriendProfile()
    {
        CreateMap<User, FriendDto>();
    }
}
