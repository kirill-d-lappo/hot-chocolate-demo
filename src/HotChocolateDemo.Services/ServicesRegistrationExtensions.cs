using FluentValidation;
using HotChocolateDemo.Services.Users;
using Microsoft.Extensions.DependencyInjection;

namespace HotChocolateDemo.Services;

public static class ServicesRegistrationExtensions
{
  public static IServiceCollection AddHCDemoServices(this IServiceCollection services)
  {
    services.AddValidatorsFromAssemblyContaining(typeof(ServicesRegistrationExtensions));

    services.AddScoped<IOldUserService, OldUserService>();

    services.AddScoped<OldUserService>();

    return services;
  }
}
