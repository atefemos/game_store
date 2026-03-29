namespace GameStore.Dtos
{
	public record class GameSummaryDto
	{
        public GameSummaryDto(int id, string name, string genre, string description, decimal price, DateOnly releaseDate)
        {
            Id = id;
            Name = name;
            Genre = genre;
            Description = description;
            Price = price;
            ReleaseDate = releaseDate;
        }

        public int Id { get; init; }
        public string Name { get; init; }
        public string Genre { get; init; }
        public string Description { get; init; }
        public decimal Price { get; init; }
        public DateOnly ReleaseDate { get; init; }
	}
}
