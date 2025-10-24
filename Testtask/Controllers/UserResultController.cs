using MongoDB.Driver;
using Microsoft.AspNetCore.Mvc;
using TestSystem.Models;
using TestSystem.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

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

    // POST: api/UserResult/Answer
    // Salva una singola risposta nel documento UserResult
    [HttpPost("Answer")]
    public async Task<IActionResult> SaveAnswer([FromBody] UserAnswer answer, [FromQuery] string userResultId)
    {
        if (answer == null || string.IsNullOrEmpty(userResultId))
            return BadRequest("Dati mancanti");

        var filter = Builders<UserResult>.Filter.Eq(r => r.Id, userResultId);
        var update = Builders<UserResult>.Update.Push(r => r.Answers, answer);

        await _mongoService.UserResults.UpdateOneAsync(filter, update);
        return Ok();
    }

    // PUT: api/UserResult/{id} -> Aggiorna il punteggio finale
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateResult(string id, [FromBody] UserResult updatedResult)
    {
        if (updatedResult == null)
            return BadRequest("Dati mancanti");

        var filter = Builders<UserResult>.Filter.Eq(r => r.Id, id);
        var update = Builders<UserResult>.Update
            .Set(r => r.CorrectAnswers, updatedResult.CorrectAnswers)
            .Set(r => r.TotalQuestions, updatedResult.TotalQuestions);

        await _mongoService.UserResults.UpdateOneAsync(filter, update);
        return Ok();
    }

    // GET: api/UserResult/{id}/Details
    // Restituisce il risultato dell'utente con il testo delle domande e delle risposte scelte
    [HttpGet("{id}/Details")]
    public IActionResult GetResultDetails(string id)
    {
        var userResult = _mongoService.UserResults.Find(r => r.Id == id).FirstOrDefault();
        if (userResult == null) return NotFound();

        var test = _mongoService.Tests.Find(t => t.Id == userResult.TestId).FirstOrDefault();
        if (test == null) return NotFound("Test non trovato");

        var detailedAnswers = userResult.Answers.Select(a =>
        {
            var question = test.Questions.FirstOrDefault(q => q.Id == a.QuestionId);
            var option = question?.Options.FirstOrDefault(o => o.Id == a.SelectedOptionId);
            return new
            {
                QuestionText = question?.Text,
                SelectedOptionText = option?.Text,
                a.IsCorrect
            };
        }).ToList();

        return Ok(new
        {
            userResult.UserName,
            userResult.CorrectAnswers,
            userResult.TotalQuestions,
            Answers = detailedAnswers
        });
    }
}
