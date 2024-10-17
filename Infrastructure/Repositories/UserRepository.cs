using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public class MongoUserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _mongoUsers;

    public MongoUserRepository(IMongoClient mongoClient, IOptions<MongoSettings> mongoSettings)
    {
        var database = mongoClient.GetDatabase(mongoSettings.Value.DatabaseName);
        _mongoUsers = database.GetCollection<User>(mongoSettings.Value.UserCollectionName);

        // Create compound index on email and createdAt for optimized querying
        var indexKeys = Builders<User>.IndexKeys
            .Ascending(user => user.Email)
            .Ascending(user => user.CreatedAt);
    }

    public async Task AddAsync(User user)
    {
        var mongoUser = new User(user.Name, user.Email, user.ConsentToDataCollection);
        await _mongoUsers.InsertOneAsync(mongoUser);
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _mongoUsers.Find(_ => true).ToListAsync();
    }

    public async Task<User> GetByIdAsync(string id)
    {
        return await _mongoUsers.Find(user => user.Id == id).FirstOrDefaultAsync();
    }
}