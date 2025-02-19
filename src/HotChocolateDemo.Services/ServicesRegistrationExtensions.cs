using FluentValidation;
using HotChocolateDemo.Services.OrderManagement.Orders;
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

    services.AddScoped<IOrderCreationService, OrderCreationService>();
    services.AddScoped<IOrderProviderService, OrderProviderService>();

    services.AddScoped<IRoleProviderService, RoleProviderService>();

    return services;
  }
}
