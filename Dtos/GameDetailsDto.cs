namespace GameStore.Dtos
{
	public record class GameDetailsDto
	{
        public GameDetailsDto(int id, string name, int genreId, string description, decimal price, DateOnly releaseDate)
        {
            Id = id;
            Name = name;
            GenreId = genreId;
            Description = description;
            Price = price;
            ReleaseDate = releaseDate;
        }

        public int Id { get; init; }
        public string Name { get; init; }
        public int GenreId { get; init; }
        public string Description { get; init; }
        public decimal Price { get; init; }
        public DateOnly ReleaseDate { get; init; }
	}
}
