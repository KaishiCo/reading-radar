using ReadingRadar.Api.Endpoints;
using ReadingRadar.Application;
using ReadingRadar.Application.Common.Interfaces.Persistence;
using ReadingRadar.Infra;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "ReadingRadar home...");
app.MapBookEndpoints();
app.MapBookStatusesEndpoints();

var dbInitializer = app.Services.GetRequiredService<IDbInitializer>();
await dbInitializer.InitializeAsync();

app.Run();
