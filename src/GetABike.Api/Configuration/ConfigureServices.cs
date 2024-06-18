using GetABike.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace GetABike.Api.Configuration;

public static class ConfigureServices
{
    public static IServiceCollection ConfigureMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(AppDomain.CurrentDomain.Load("GetABike.Application")));

        return services;
    }

    public static IServiceCollection ConfigureApplicationContext(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Postgress");
        services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connectionString));

        return services;
    }
}