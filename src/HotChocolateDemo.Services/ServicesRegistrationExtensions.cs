using FluentValidation;
using HotChocolateDemo.Services.Roles;
using HotChocolateDemo.Services.Users;
using Microsoft.Extensions.DependencyInjection;

namespace HotChocolateDemo.Services;

public static class ServicesRegistrationExtensions
{
  public static IServiceCollection AddHCDemoServices(this IServiceCollection services)
  {
    services.AddValidatorsFromAssemblyContaining(typeof(ServicesRegistrationExtensions));

    services.AddScoped<IOldUserService, OldUserService>();
    services.AddScoped<IUserProviderService, UserProviderService>();
    services.AddScoped<IUserCreationService, UserCreationService>();

    services.AddScoped<IRoleProviderService, RoleProviderService>();

    return services;
  }
}
