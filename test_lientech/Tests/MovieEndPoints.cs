using Microsoft.AspNetCore.Http.HttpResults;
using test_lientech.Model;

namespace test_lientech.Tests
{
    public class MovieEndPoints
    {
        public static Created<Movie> AddMovie(Movie movie, MovieDbContext db)
        {
            db.Movie.Add(movie);
            db.SaveChanges();
            return TypedResults.Created($"/movie/{movie.MovieId}", movie);
        }
    }
}
