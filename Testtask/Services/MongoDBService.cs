// Libreria ufficiale per interagire con MongoDB in .NET
using MongoDB.Driver;

// Namespace dei modelli dell’applicazione (Test e UserResult)
using TestSystem.Models;

// Libreria per leggere i valori di configurazione da appsettings.json
using Microsoft.Extensions.Configuration; // <- ci serve per IConfiguration

namespace TestSystem.Services;

// Servizio che incapsula la connessione e l’accesso al database MongoDB

    // Costruttore del servizio
    // Riceve IConfiguration tramite dependency injection
    // Questo ci permette di leggere facilmente la connection string e il database dal file di configurazione
public class MongoDBService : IMongoDBService
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
