using MongoDB.Driver;
using Microsoft.AspNetCore.Mvc;
using TestSystem.Models;
using TestSystem.Services;


namespace TestSystem.Controllers;
//controlloer: gestisce chiamate HTTP/Espone due endpoint
//Service: Il controller si appoggia a un servizio MongoDBService, iniettato nel costruttore. acaPrincipio di inversione delle dipendenze: il controller non si preoccupa di come MongoDB è configurato, ma usa un servizio già pronto

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
    //operazioni CRUD Fa una query su MongoDB (Find(_ => true)) per ottenere tutti i documenti nella collezione.Restituisce il risultato con Ok(tests) → HTTP 200.
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
