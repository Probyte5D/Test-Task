using MongoDB.Driver;
using Microsoft.AspNetCore.Mvc;
using TestSystem.Models;
using TestSystem.Services;


namespace TestSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserResultController : ControllerBase
{
    private readonly MongoDBService _mongoService;

    public UserResultController(MongoDBService mongoService)
    {
        _mongoService = mongoService;
    }

    // GET: api/UserResult
    [HttpGet]
    public IActionResult GetAllResults()
    {
        var results = _mongoService.UserResults.Find(_ => true).ToList();
        return Ok(results);
    }

    // POST: api/UserResult
    [HttpPost]
    public IActionResult CreateResult([FromBody] UserResult result)
    {
        _mongoService.UserResults.InsertOne(result);
        return Ok(result);
    }
}
