using BuberDiner.Application.Authentication.Commands.Register;
using BuberDiner.Application.Authentication.Common;
using BuberDiner.Application.Common.Behaviours;
using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BuberDiner.Application;

    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehaviour<,>));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
        }
    }

