// Importiamo le librerie necessarie
using MongoDB.Driver;
using Microsoft.AspNetCore.Mvc;
using TestSystem.Models;
using TestSystem.Services;

namespace TestSystem.Controllers;
// Dichiariamo che questa classe è un Controller API
// Imposta la route base per gli endpoint di questo controller: "api/UserResult"

[ApiController]
[Route("api/[controller]")]
public class UserResultController : ControllerBase
{
    private readonly MongoDBService _mongoService;

    public UserResultController(MongoDBService mongoService)
    {
        _mongoService = mongoService;
    }

    // ------------------------------
    // ENDPOINT GET → api/UserResult
    // ------------------------------
    // GET: api/UserResult
    [HttpGet]
    public IActionResult GetAllResults()
    {
        try
        {
            // Recupera tutti i documenti della collezione "UserResults"
            var results = _mongoService.UserResults.Find(_ => true).ToList();
            // Restituisce i risultati con stato HTTP 200 (OK)
            return Ok(results);
        }
        catch (MongoException ex)
        {
            // Gestione errore specifico MongoDB
            return StatusCode(500, $"Errore di connessione al database: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Gestione di eventuali altri errori generici
            return StatusCode(500, $"Si è verificato un errore: {ex.Message}");
        }
    }

    // ------------------------------
    // ENDPOINT POST → api/UserResult
    // ------------------------------
    // POST: api/UserResult
    [HttpPost]
    public IActionResult CreateResult([FromBody] UserResult result)
    {
        try
        {
            // Validazione del modello
            if (result == null)
                return BadRequest("Il corpo della richiesta non può essere vuoto.");

            // Con CreateResult aggiungo un nuovo risultato al database
            _mongoService.UserResults.InsertOne(result);

            // Restituisce l’oggetto appena creato con codice HTTP 201 (Created)
            return CreatedAtAction(nameof(GetAllResults), new { id = result.Id }, result);
        }
        catch (MongoWriteException ex)
        {
            // Gestione errore di scrittura su MongoDB (es. duplicati)
            return StatusCode(500, $"Errore durante l'inserimento nel database: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Gestione di eventuali altri errori generici
            return StatusCode(500, $"Si è verificato un errore: {ex.Message}");
        }
    }
}
