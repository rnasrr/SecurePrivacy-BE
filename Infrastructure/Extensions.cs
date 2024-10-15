using Domain.Repositories;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Configuration;

namespace Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Bind IUserRepository to MongoUserRepository (decoupled from MongoDB specifics)
        services.AddScoped<IUserRepository, MongoUserRepository>();

        // Fill Mongo Settings with data
        services.Configure<MongoSettings>(options =>
        {
            configuration.GetSection("MongoSettings").Bind(options);
        });

        // Add Database
        services.AddSingleton<MongoDbContext>();

        // Register MongoClient as a singleton for additional flexibility
        services.AddSingleton<IMongoClient>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<MongoSettings>>().Value;
            return new MongoClient(settings.ConnectionString);
        });

        return services;
    }
}
