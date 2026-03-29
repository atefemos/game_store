using System.ComponentModel.DataAnnotations;

namespace GameStore.Dtos
{
    public record class UpdateGameDto(
        [Required][StringLength(50)] string Name,
        int GenreId,
        [StringLength(200)] string Description,
        [Range(1, 100)] decimal Price,
        [Required] DateOnly ReleaseDate);
}