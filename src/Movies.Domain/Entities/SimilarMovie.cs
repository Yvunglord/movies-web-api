namespace Movies.Domain.Entities
{
    public class SimilarMovie
    {
        public int MovieId { get; set; }
        public int SimilarMovieId { get; set; }
        public double Score { get; set; }
    }
}