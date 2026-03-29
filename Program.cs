using GameStore.Endpoints;
using GameStore.Entities;
using GameStore.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("GameStore") ?? throw new InvalidOperationException("Connection string 'GameStore' not found.");
builder.Services.AddSqlite<GameStoreContext>(connectionString);

var app = builder.Build();
app.MapGamesEndpoints();

await app.MigrateDbAsync();

app.Run();
