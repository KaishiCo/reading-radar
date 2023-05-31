using ReadingRadar.Api;
using ReadingRadar.Api.Endpoints;
using ReadingRadar.Application;
using ReadingRadar.Application.Common.Interfaces.Persistence;
using ReadingRadar.Infra;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.MapHealthChecks("/_health");
app.MapBookEndpoints();
app.MapBookStatusesEndpoints();
app.MapSeriesEndpoints();

var dbInitializer = app.Services.GetRequiredService<IDbInitializer>();
await dbInitializer.InitializeAsync();

app.Run();
