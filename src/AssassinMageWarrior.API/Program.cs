using AssassinMageWarrior.Domain.Handlers.Auth;
using AssassinMageWarrior.Domain.Handlers.Relationship;
using AssassinMageWarrior.Domain.Handlers.Room;
using AssassinMageWarrior.Domain.Services;
using AssassinMageWarrior.Shareable.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://*:5114");

// Add services to the container.
var key = builder.Configuration.GetValue<string>("Keys:Secret");
builder.Services.AddScoped(option => new EncryptPassword(key!));
builder.Services.AddScoped(option => new JwtGenerate(key!));

builder.Services.ConfigureServices(builder.Configuration);

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(RegisterHandler).Assembly);
    config.RegisterServicesFromAssembly(typeof(LoginHandler).Assembly);
    config.RegisterServicesFromAssembly(typeof(AddFriendHandler).Assembly);
    config.RegisterServicesFromAssembly(typeof(ReadFriendListHandler).Assembly);
    config.RegisterServicesFromAssembly(typeof(LoggedHandler).Assembly);
    config.RegisterServicesFromAssembly(typeof(InactiveUserHandler).Assembly);
    config.RegisterServicesFromAssembly(typeof(OpenRoomHandler).Assembly);
    config.RegisterServicesFromAssembly(typeof(CloseRoomHandler).Assembly);
    config.RegisterServicesFromAssembly(typeof(UsersInRoomHandler).Assembly);
    config.RegisterServicesFromAssembly(typeof(InviteToRoomHandler).Assembly);
    config.RegisterServicesFromAssembly(typeof(ReceiveInviteHandler).Assembly);
    config.RegisterServicesFromAssembly(typeof(AcceptInviteHandler).Assembly);
    config.RegisterServicesFromAssembly(typeof(CancelInviteHandler).Assembly);
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var tokenKey = Encoding.ASCII.GetBytes(key!);
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(tokenKey),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();
builder.Services.AddSignalR();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowAll", builder =>
    {
        builder.SetIsOriginAllowed(_ => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT no campo a seguir."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.SetMinimumLevel(LogLevel.Information);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program();