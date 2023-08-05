using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Naggaro.AccountStatment.Application.Common.Behaviours;

namespace Naggaro.AccountStatment.Application;

public static class RegisterApplication
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assemply = typeof(RegisterApplication).Assembly;
        services.AddMediatR(config => {
            config.RegisterServicesFromAssembly(assemply);
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
          //  config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
        })  ;

        services.AddValidatorsFromAssembly(assemply);

        return services;
    }


 }

