using Domain.Entities;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Database;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;
    private readonly IOptions<MongoSettings> _mongoSettings;
    private readonly IMongoCollection<User> _userCollection;
    public MongoDbContext(IOptions<MongoSettings> mongoSettings)
    {
        _mongoSettings = mongoSettings;
        var client = new MongoClient(_mongoSettings.Value.ConnectionString);
        _database = client.GetDatabase(_mongoSettings.Value.DatabaseName);
        _userCollection = _database.GetCollection<User>(_mongoSettings.Value.UserCollectionName);
    }

    // This method creates indexes for the User collection.
    private void CreateIndexes()
    {
        var emailIndexKeys = Builders<User>.IndexKeys.Ascending(u => u.Email);
        var nameIndexKeys = Builders<User>.IndexKeys.Ascending(u => u.Name);


        var emailIndex = new CreateIndexModel<User>(emailIndexKeys, new CreateIndexOptions
        {
            Unique = true, 
            Name = "Email_Index"
        });

        var indexModel = new CreateIndexModel<User>(nameIndexKeys, new CreateIndexOptions
        {
            Unique = false,
            Name = "Name_Index"
        });

        _userCollection.Indexes.CreateOne(indexModel);
    }

    // Expose collections as properties
    public IMongoCollection<User> Users => _userCollection;
}
