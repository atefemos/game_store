using gameStore.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<GameDto> games = new List<GameDto>
{
    new GameDto { Id = 1, Name = "Game 1", Genre = "Action", Description = "An action-packed adventure.", Price = 59.99m, ReleaseDate = new DateOnly(2023, 5, 1) },
    new GameDto { Id = 2, Name = "Game 2", Genre = "RPG", Description = "A role-playing game with an immersive story.", Price = 49.99m, ReleaseDate = new DateOnly(2023, 6, 15) },
    new GameDto { Id = 3, Name = "Game 3", Genre = "Strategy", Description = "A strategic game that challenges your mind.", Price = 39.99m, ReleaseDate = new DateOnly(2023, 7, 20) }
};

app.MapGet("/", () => "Hello World!");
app.MapGet("/games", () => games);

app.Run();
