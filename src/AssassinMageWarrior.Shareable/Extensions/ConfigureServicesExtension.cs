using AssassinMageWarrior.Data;
using AssassinMageWarrior.Data.Repository.Auth.Logged;
using AssassinMageWarrior.Data.Repository.Auth.Login;
using AssassinMageWarrior.Data.Repository.Auth.Register;
using AssassinMageWarrior.Data.Repository.Relationship.AddFriend;
using AssassinMageWarrior.Data.Repository.Relationship.InactiveUser;
using AssassinMageWarrior.Data.Repository.Relationship.ReadFriendList;
using AssassinMageWarrior.Data.Repository.Room.AcceptInvite;
using AssassinMageWarrior.Data.Repository.Room.CancelInvite;
using AssassinMageWarrior.Data.Repository.Room.CloseRoom;
using AssassinMageWarrior.Data.Repository.Room.InviteToRoom;
using AssassinMageWarrior.Data.Repository.Room.OpenRoom;
using AssassinMageWarrior.Data.Repository.Room.ReceiveInvite;
using AssassinMageWarrior.Data.Repository.Room.UserInRoom;
using AssassinMageWarrior.Shareable.Profiles;
using AssassinMageWarrior.Shareable.Profiles.Auth;
using AssassinMageWarrior.Shareable.Profiles.Room;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AssassinMageWarrior.Shareable.Extensions;

public static class ConfigureServicesExtension
{
    public static void ConfigureServices(this IServiceCollection service, IConfiguration configuration)
    {
        AddDbContext(service, configuration);
        AddRepositories(service);
        AddMapper(service);
    }

    private static void AddDbContext(IServiceCollection service, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Google");
        service.AddDbContext<Context>(options => options.UseNpgsql(connectionString).UseLazyLoadingProxies());
        service.AddIdentityCore<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<Context>();
    }

    private static void AddRepositories(IServiceCollection service)
    {
        service.AddScoped<IRegisterRepository, RegisterRepository>();
        service.AddScoped<ILoginRepository, LoginRepository>();
        service.AddScoped<IAddFriendRepository, AddFriendRepository>();
        service.AddScoped<IReadFriendListRepository, ReadFriendListRepository>();
        service.AddScoped<IInactiveUserRepository, InactiveUserRepository>();
        service.AddScoped<ILoggedRepository, LoggedRepository>();
        service.AddScoped<IOpenRoomRepository, OpenRoomRepository>();
        service.AddScoped<ICloseRoomRepository, CloseRoomRepository>();
        service.AddScoped<IUserInRoomRepository, UserInRoomRepository>();
        service.AddScoped<IInviteToRoomRepository, InviteToRoomRepository>();
        service.AddScoped<IReceiveInviteRepository, ReceiveInviteRepository>();
        service.AddScoped<IAcceptInviteRepository, AcceptInviteRepository>();
        service.AddScoped<ICancelInviteRepository, CancelInviteRepository>();
    }

    private static void AddMapper(IServiceCollection service)
        => service.AddAutoMapper(options =>
        {
            options.AddProfile(new RegisterProfile());
            options.AddProfile(new LoginProfile());
            options.AddProfile(new FriendProfile());
            options.AddProfile(new InviteProfile());
        }
        );
}
