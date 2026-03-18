using GameStore.Dtos;

namespace GameStore.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGameById";
   private static readonly List<GameDto> games =
[
   new (  1, "Game 1", "Action", "An action-packed adventure.", 59.99m, new DateOnly(2023, 5, 1) ),
   new (  2, "Game 2", "RPG", "A role-playing game with an immersive story.", 49.99m, new DateOnly(2023, 6, 15) ),
   new (  3, "Game 3", "Strategy", "A strategic game that challenges your mind.", 39.99m, new DateOnly(2023, 7, 20) )
];

public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
{
    var group = app.MapGroup("games").WithParameterValidation();

    group.MapGet("/", () => games);

    // Endpoint to get game by ID
    group.MapGet("/{id}", (int id) => { 
        GameDto? game = games.Find(g => g.Id == id); 
        return game is not null ? Results.Ok(game) : Results.NotFound();
    }).WithName(GetGameEndpointName);

    // Endpoint to add a new game
    group.MapPost("/",(CreateGameDto newGame)=>
    {
       GameDto game = new(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Description,
        newGame.Price,
        newGame.ReleaseDate
       );

       games.Add(game);
       return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
    });

    // Endpoint to update an existing game
    group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
    {
        var game = games.Find(g => g.Id == id);
        if (game is null)
        {
            return Results.NotFound();
        }

        var index = games.IndexOf(game);

        games[index] = new GameDto(
            id,
            updatedGame.Name,
            updatedGame.Genre,
            updatedGame.Description,
            updatedGame.Price,
            updatedGame.ReleaseDate
        );

        return Results.NoContent();
    });

    return group;
}}