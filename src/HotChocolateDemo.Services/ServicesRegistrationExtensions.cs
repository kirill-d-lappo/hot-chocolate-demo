using FluentValidation;
using HotChocolateDemo.Services.UserManagement.Roles;
using HotChocolateDemo.Services.UserManagement.Users;
using Microsoft.Extensions.DependencyInjection;

namespace HotChocolateDemo.Services;

public static class ServicesRegistrationExtensions
{
  public static IServiceCollection AddHCDemoServices(this IServiceCollection services)
  {
    services.AddValidatorsFromAssemblyContaining(typeof(ServicesRegistrationExtensions));

    services.AddScoped<IUserProviderService, UserProviderService>();
    services.AddScoped<IUserCreationService, UserCreationService>();
    services.AddScoped<IUserUpdateService, UserUpdateService>();
    services.AddScoped<IUserImageUpdateService, UserImageUpdateService>();
    services.AddScoped<IUserImageStorage, LocalUserImageStorage>();

    services.AddScoped<IRoleProviderService, RoleProviderService>();

    return services;
  }
}
