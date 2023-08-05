using Naggaro.AccountStatment.Application;
using Naggaro.AccountStatment.Application.Common.Interfaces;
using Naggaro.AccountStatment.Infrastructure;
using Naggaro.AccountStatment.Infrastructure.Data;
using Naggaro.AccountStatment.WebApi.Infrastructure;
using Naggaro.AccountStatment.WebApi.Services;

namespace Naggaro.AccountStatment.WebApi;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddScoped<IUser, CurrentUser>();

        builder.Services.AddApplication().AddInfrastructure();
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddControllers(options =>
          options.Filters.Add<ApiExceptionFilter>());


        var app = builder.Build();

        await app.InitialiseDatabaseAsync();


        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();
        app.UseAuthorization();
        app.MapGet("/", () => "Hello World!");

        app.MapControllers();

        app.Run();
    }
}
