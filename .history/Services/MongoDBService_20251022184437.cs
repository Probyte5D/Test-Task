using MongoDB.Driver;
using TestSystem.Models;

namespace TestSystem.Services;

public class MongoDBService
{
    private readonly IMongoDatabase _database;

    public MongoDBService(IConfiguration config)
    {
        var client = new MongoClient(config["MongoDB:ConnectionString"]);
        _database = client.GetDatabase(config["MongoDB:DatabaseName"]);
    }

    public IMongoCollection<Test> Tests => _database.GetCollection<Test>("Tests");
    public IMongoCollection<UserResult> UserResults => _database.GetCollection<UserResult>("UserResults");
}
