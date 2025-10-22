using TestSystem.Services;

var builder = WebApplication.CreateBuilder(args);

// Aggiunge supporto ai controller
builder.Services.AddControllers();

// Aggiunge Swagger per la documentazione API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Registra il nostro servizio MongoDB
builder.Services.AddSingleton<MongoDBService>();

// ✅ Abilita CORS per permettere al frontend di fare fetch
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configura la pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ✅ Abilita CORS
app.UseCors();

app.UseAuthorization();

// Serve file HTML, CSS, JS da wwwroot
app.UseStaticFiles();

// ✅ Mappa i controller
app.MapControllers();

app.Run();
