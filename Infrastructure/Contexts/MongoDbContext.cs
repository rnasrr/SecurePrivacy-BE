using Domain.Entities;
using Infrastructure.Settings;
using MongoDB.Driver;

namespace Infrastructure.Database;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(MongoSettings mongoSettings)
    {
        var client = new MongoClient(mongoSettings.ConnectionString);
        _database = client.GetDatabase(mongoSettings.DatabaseName);
    }

    // Expose collections as properties
    public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
}
