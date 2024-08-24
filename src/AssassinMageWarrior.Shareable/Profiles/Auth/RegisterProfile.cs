using AssassinMageWarrior.Data.Entities;
using AssassinMageWarrior.Shareable.Dto.Auth;
using AutoMapper;

namespace AssassinMageWarrior.Shareable.Profiles.Auth;

public class RegisterProfile : Profile
{
    public RegisterProfile() => CreateMap<RegisterDto, User>();
}
