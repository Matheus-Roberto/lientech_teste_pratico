using AWSELOAPI.ViewModel;
using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore;
using System.Data;
using test_lientech.Data;
using test_lientech.Model;

namespace test_lientech.Service
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApiDbContext _apiDbContext;

        public MovieRepository(ApiDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext ?? throw new ArgumentNullException(nameof(apiDbContext));
        }

        public async Task<Movie> Create(MovieRequestViewModel movieViewModel)
        {
            var movie = new Movie(movieViewModel.Name,movieViewModel.Director,movieViewModel.Duration);
            _apiDbContext.Movie.Add(movie);
            await _apiDbContext.SaveChangesAsync();
            return movie;
        }

        public async Task<Movie> DeleteById(int movieId)
        {
            var movie = _apiDbContext.Movie.Find(movieId);
            var result = _apiDbContext.Remove(movieId);
            await _apiDbContext.SaveChangesAsync();
            if (result != null)
            {
                return movie;
            }
            return null;
        }

        public MovieResponseViewModel GetAll(int pageNumber, int pageQuantity)
        {
            MovieResponseViewModel responseViewModel = new MovieResponseViewModel();
            responseViewModel.MovieList = _apiDbContext.Movie.Skip((pageNumber - 1) * pageQuantity).Take(pageQuantity).ToList();
            if (!responseViewModel.MovieList.Any())
            {
                throw new DBConcurrencyException("erro ao acessar o banco de dados, não foi possível pegar a lista de Vehicle");
            }
            responseViewModel.Meta = new Meta();
            responseViewModel.Meta.Total = _apiDbContext.Movie.Count();
            if (responseViewModel.Meta.Total == 0)
            {
                throw new DBConcurrencyException("erro ao acessar o banco de dados, erro no numero de Vehicle");
            }
            responseViewModel.Meta.LastPage = (int)Math.Ceiling(Convert.ToDecimal(responseViewModel.Meta.Total) / Convert.ToDecimal(pageQuantity));
            responseViewModel.Meta.CurrentPage = pageNumber;
            responseViewModel.Meta.PerPage = pageQuantity;
            responseViewModel.Meta.Prev = (pageNumber == 1) ? 1 : pageNumber - 1;
            responseViewModel.Meta.Next = (pageNumber == responseViewModel.Meta.LastPage) ? responseViewModel.Meta.LastPage : pageNumber + 1;
            return responseViewModel;
        }

        public Movie? GetById(int movieId)
        {
            return _apiDbContext.Movie.Find(movieId);
        }

        public async Task<Movie> Update(Movie movie)
        {
            _apiDbContext.Movie.Update(movie);
            await _apiDbContext.SaveChangesAsync();
            return movie;
        }
    }
}
