using AssassinMageWarrior.Data.Entities;
using AssassinMageWarrior.Shareable.Dto.Auth;
using AutoMapper;

namespace AssassinMageWarrior.Shareable.Profiles.Auth;

public class LoginProfile : Profile
{
    public LoginProfile()
    {
        CreateMap<LoginDto, User>().ForMember(a => a.Email, options => options.MapFrom(a => a.Email));
    }
}
