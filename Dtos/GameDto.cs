namespace gameStore.Dtos
{
	public record class GameDto
	{
		public int Id { get; init; }
        public string Name { get; init; }
        public string Genre { get; init; }
        public string Description { get; init; }
        public decimal Price { get; init; }
        public DateOnly ReleaseDate { get; init; }
	}
}
