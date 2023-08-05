using Naggaro.AccountStatment.Application;
using Naggaro.AccountStatment.Application.Common.Interfaces;
using Naggaro.AccountStatment.Infrastructure;
using Naggaro.AccountStatment.WebApi.Infrastructure;
using Naggaro.AccountStatment.WebApi.Services;

namespace Naggaro.AccountStatment.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        builder.Services.AddScoped<IUser, CurrentUser>();

        builder.Services.AddApplication().AddInfrastructure();
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddControllers(options =>
          options.Filters.Add<ApiExceptionFilter>());

        app.MapGet("/", () => "Hello World!");

        app.Run();
    }
}
