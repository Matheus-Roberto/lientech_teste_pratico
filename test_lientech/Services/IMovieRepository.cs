using test_lientech.Model;

namespace test_lientech.Service
{
    public interface IMovieRepository
    {
        Task<Movie> Create(MovieRequestViewModel movieViewModel);
        Task<Movie> Update(Movie movie);
        Task<Movie> DeleteById(int movieId);
        Movie? GetById(int movieId);
        MovieResponseViewModel GetAll(int pageNumber, int pageQuantity);
    }
}
