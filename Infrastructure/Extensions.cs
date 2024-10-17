using Domain.Repositories;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;


namespace Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IMongoClient>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<MongoSettings>>().Value;
            return new MongoClient(settings.ConnectionString);
        });

        // Register MongoDbContext
        services.AddSingleton<MongoDbContext>();

        services.AddTransient<IUserRepository, MongoUserRepository>();

        return services;
    }
}
