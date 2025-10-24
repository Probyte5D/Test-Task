using MongoDB.Driver;
using Microsoft.AspNetCore.Mvc;
using TestSystem.Models;
using TestSystem.Services;

namespace TestSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserResultController : ControllerBase
{
    private readonly IMongoDBService _mongoService;

    public UserResultController(IMongoDBService mongoService)
    {
        _mongoService = mongoService;
    }

    // GET: api/UserResult
    [HttpGet]
    public IActionResult GetAllResults()
    {
        try
        {
            var results = _mongoService.UserResults.Find(_ => true).ToList();
            return Ok(results);
        }
        catch (MongoException ex)
        {
            return StatusCode(500, $"Errore di connessione al database: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Si è verificato un errore: {ex.Message}");
        }
    }

    // POST: api/UserResult
    [HttpPost]
    public IActionResult CreateResult([FromBody] UserResult result)
    {
        try
        {
            if (result == null)
                return BadRequest("Il corpo della richiesta non può essere vuoto.");

            _mongoService.UserResults.InsertOne(result);

            return CreatedAtAction(nameof(GetAllResults), new { id = result.Id }, result);
        }
        catch (MongoWriteException ex)
        {
            return StatusCode(500, $"Errore durante l'inserimento nel database: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Si è verificato un errore: {ex.Message}");
        }
    }
}
