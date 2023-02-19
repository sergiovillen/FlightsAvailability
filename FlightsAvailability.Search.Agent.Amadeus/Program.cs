using FlightsAvailability.Search.Agent.Amadeus;
using Healthchecks;
using HealthChecks.UI.Client;

var builder = WebApplication.CreateBuilder(args);
//Dapr
builder.AddCustomConfiguration();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Dapr
builder.AddCustomMvc();
builder.AddCustomHealthChecks();
//Application Services
builder.AddCustomApplicationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCloudEvents();
app.UseAuthorization();
app.UseCors("CorsPolicy");
app.MapDefaultControllerRoute();
app.MapControllers();
app.MapSubscribeHandler();
app.MapCustomHealthChecks("/hc", "/liveness", UIResponseWriter.WriteHealthCheckUIResponse);

app.Run();
