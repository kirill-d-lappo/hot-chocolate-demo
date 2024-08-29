using FluentValidation;
using HotChocolateDemo.Services.Users;
using Microsoft.Extensions.DependencyInjection;

namespace HotChocolateDemo.Services;

public static class ServicesRegistrationExtensions
{
  public static IServiceCollection AddHCDemoServices(this IServiceCollection services)
  {
    services.AddValidatorsFromAssemblyContaining(typeof(IUserService));

    services.AddScoped<IUserService, UserService>();

    return services;
  }
}
