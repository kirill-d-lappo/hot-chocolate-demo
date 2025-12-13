using FluentValidation;
using HCDemo.Services.OrderManagement.Foods;
using HCDemo.Services.OrderManagement.Orders;
using HCDemo.Services.UserManagement.Roles;
using HCDemo.Services.UserManagement.Users;
using Microsoft.Extensions.DependencyInjection;

namespace HCDemo.Services;

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

    services.AddScoped<IFoodProviderService, FoodProviderService>();

    services.AddScoped<IRoleProviderService, RoleProviderService>();

    return services;
  }
}
