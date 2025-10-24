// Libreria ufficiale per interagire con MongoDB in .NET
using MongoDB.Driver;

// Namespace dei modelli dell’applicazione (Test e UserResult)
using TestSystem.Models;

// Libreria per leggere i valori di configurazione da appsettings.json
using Microsoft.Extensions.Configuration; // <- ci serve per IConfiguration

namespace TestSystem.Services;

// Servizio che incapsula la connessione e l’accesso al database MongoDB
public class MongoDBService
{
    // Riferimento al database MongoDB a cui ci connettiamo
    private readonly IMongoDatabase _database;

    // Costruttore del servizio
    // Riceve IConfiguration tramite dependency injection
    // Questo ci permette di leggere facilmente la connection string e il database dal file di configurazione
    public MongoDBService(IConfiguration config)
    {
        // Creiamo un client MongoDB usando la connection string dal file appsettings.json
        var client = new MongoClient(config["MongoDB:ConnectionString"]);

        // Selezioniamo il database da usare, sempre leggendo il nome dal file di configurazione
        _database = client.GetDatabase(config["MongoDB:DatabaseName"]);
    }

    // Espone la collezione "Tests" come proprietà pubblica
    // I controller possono usarla per leggere, inserire o modificare i documenti dei Test
    public IMongoCollection<Test> Tests => _database.GetCollection<Test>("Tests");

    // Espone la collezione "UserResults" come proprietà pubblica
    // I controller possono usarla per leggere, inserire o modificare i risultati degli utenti
    public IMongoCollection<UserResult> UserResults => _database.GetCollection<UserResult>("UserResults");
}
