using ReadingRadar.Api.Endpoints;
using ReadingRadar.Application;
using ReadingRadar.Infra;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "ReadingRadar home...");
app.MapBookEndpoints();

app.Run();
