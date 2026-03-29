using GameStore.Dtos;
using GameStore.Entities;
using GameStore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Endpoints;

public static class GamesEndpoints
{
const string GetGameEndpointName = "GetGameById";

public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
{
    var group = app.MapGroup("games").WithParameterValidation();

    group.MapGet("/", async (GameStoreContext dbContext) => await dbContext.Games.Include(g => g.Genre)
        .Select(g => g.ToGameSummaryDto())
        .AsNoTracking()
        .ToListAsync())
        .WithName("GetAllGames");

        // Endpoint to get game by ID
    group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
    {
        Game? game = await dbContext.Games.FindAsync(id);
        return game is not null ? Results.Ok(game.ToGameDetailsDto()) : Results.NotFound();
    }).WithName(GetGameEndpointName);
    
    // Endpoint to delete a game by ID
    group.MapDelete("/{id}", async(int id, GameStoreContext dbContext) =>
    {
        int rowsAffected = await dbContext.Games.Where(game => game.Id == id).ExecuteDeleteAsync();
        if (rowsAffected == 0)
    {
        return Results.NotFound();
    }

        dbContext.SaveChanges();

        return Results.NoContent();
    });

    // Endpoint to add a new game
    group.MapPost("/", async (CreateGameDto newGame, GameStoreContext dbContext) =>
    {
        Game game = newGame.ToEntity();
       
        dbContext.Games.Add(game);
        await dbContext.SaveChangesAsync();
       
       return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game.ToGameDetailsDto());
    });

    // Endpoint to update an existing game
    group.MapPut("/{id}", async(int id, UpdateGameDto updatedGame, GameStoreContext dbContext) =>
    {
        var existingGame = await dbContext.Games.FindAsync(id);
        if (existingGame is null)
        {
            return Results.NotFound();
        }

        dbContext.Entry(existingGame).CurrentValues.SetValues(updatedGame.ToEntity(id));
        await dbContext.SaveChangesAsync();

        return Results.NoContent();
    });

    return group;
}}