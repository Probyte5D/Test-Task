using MongoDB.Driver;
using Microsoft.AspNetCore.Mvc;
using TestSystem.Models;
using TestSystem.Services;


namespace TestSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private readonly MongoDBService _mongoService;

    public TestController(MongoDBService mongoService)
    {
        _mongoService = mongoService;
    }

    // GET: api/test
    [HttpGet]
    public IActionResult GetAllTests()
    {
        var tests = _mongoService.Tests.Find(_ => true).ToList();
        return Ok(tests);
    }

    // POST: api/test
    [HttpPost]
    public IActionResult CreateTest([FromBody] Test test)
    {
        _mongoService.Tests.InsertOne(test);
        return Ok(test);
    }
}
