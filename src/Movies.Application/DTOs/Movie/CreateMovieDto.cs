using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Movies.Domain.Entities;

namespace Movies.Application.DTOs.Movie
{
    public class CreateMovieDto
    { 
        [Required]
        public string Title { get; set; } = string.Empty;
        public string Director { get; set; } = string.Empty;
        public double Rating { get; set; }
        public IEnumerable<Human> Actors { get; set; } = new List<Human>();
        public IEnumerable<Tag> Tags { get; set; } = new List<Tag>();
    }
}