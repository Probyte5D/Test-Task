using TestSystem.Services;

var builder = WebApplication.CreateBuilder(args);

// Aggiunge supporto ai controller
builder.Services.AddControllers();

// Aggiunge Swagger per la documentazione API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Registra il nostro servizio MongoDB
builder.Services.AddSingleton<MongoDBService>();

var app = builder.Build();

// Configura la pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// ✅ Mappa i controller
app.MapControllers();

app.UseStaticFiles(); // Serve file HTML, CSS, JS da wwwroot


app.Run();
