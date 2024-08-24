using AssassinMageWarrior.Data.Entities;
using AssassinMageWarrior.Shareable.Dto.Room;
using AutoMapper;

namespace AssassinMageWarrior.Shareable.Profiles.Room;

public class InviteProfile : Profile
{
    public InviteProfile()
    {
        CreateMap<Invite, InviteDto>();
    }
}
