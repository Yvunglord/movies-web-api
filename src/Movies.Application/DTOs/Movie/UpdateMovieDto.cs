using System.ComponentModel.DataAnnotations;

namespace Movies.Application.DTOs.Movie
{
    public class UpdateMovieDto : CreateMovieDto
    { 
        [Required]
        public int Id { get; set; }
    }
}