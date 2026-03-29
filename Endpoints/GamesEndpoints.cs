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

    group.MapGet("/", (GameStoreContext dbContext) => dbContext.Games.Include(g => g.Genre).Select(g => g.ToGameSummaryDto()).AsNoTracking());

        // Endpoint to get game by ID
        group.MapGet("/{id}", (int id, GameStoreContext dbContext) =>
        {
            Game? game = dbContext.Games.Find(id);
            return game is not null ? Results.Ok(game.ToGameDetailsDto()) : Results.NotFound();
        }).WithName(GetGameEndpointName);
    
    // Endpoint to delete a game by ID
    group.MapDelete("/{id}", (int id, GameStoreContext dbContext) =>
    {
        int rowsAffected = dbContext.Games.Where(game => game.Id == id).ExecuteDelete();
        if (rowsAffected == 0)
    {
        return Results.NotFound();
    }

        dbContext.SaveChanges();

        return Results.NoContent();
    });

    // Endpoint to add a new game
    group.MapPost("/",(CreateGameDto newGame, GameStoreContext dbContext)=>
    {
        Game game = newGame.ToEntity();
       
        dbContext.Games.Add(game);
        dbContext.SaveChanges();
       
       return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game.ToGameDetailsDto());
    });

    // Endpoint to update an existing game
    group.MapPut("/{id}", (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) =>
    {
        var existingGame = dbContext.Games.Find(  id);
        if (existingGame is null)
        {
            return Results.NotFound();
        }

        dbContext.Entry(existingGame).CurrentValues.SetValues(updatedGame.ToEntity(id));
        dbContext.SaveChanges();

        return Results.NoContent();
    });

    return group;
}}