using MongoDB.Driver;
using TestSystem.Models;

namespace TestSystem.Services
{
    public interface IMongoDBService
    {
        IMongoCollection<Test> Tests { get; }
        IMongoCollection<UserResult> UserResults { get; }
    }
}
