using BuberDiner.Api.Common.Errors;
using BuberDiner.Api.Common.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BuberDiner.Api;

    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
        services.AddControllers();

        services.AddSingleton<ProblemDetailsFactory, BuberDinerProblemDetailsFactory>();
        services.AddMapping();
        return services;
        }
    }

