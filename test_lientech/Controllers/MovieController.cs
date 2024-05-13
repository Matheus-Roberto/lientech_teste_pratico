using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using test_lientech.Model;
using test_lientech.Service;

namespace test_lientech.Controllers
{
    
    [ApiController]
    [Route("api/v1/movie")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public MovieController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
        }

        [HttpPost]
        public async Task<IActionResult> AddMovie(MovieRequestViewModel movie)
        {
            var response = await _movieRepository.Create(movie);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMovie(Movie movie)
        {
            var response = await _movieRepository.Update(movie);

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMovie(int movieId)
        {
            var response = await _movieRepository.DeleteById(movieId);

            return Ok(response);
        }

        [HttpGet]
        [Route("Get")]
        public IActionResult Get(int pageNumber, int pageQuantity)
        {
            if (pageNumber == 0 || pageQuantity == 0)
            {
                return BadRequest("numero de paginação ou a quantidade de pagina é 0");
            }
            var movie = _movieRepository.GetAll(pageNumber, pageQuantity);
            if (movie is null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(int movieId)
        {
            if (movieId == 0)
            {
                return BadRequest("id é 0");
            }
            var movie = _movieRepository.GetById(movieId);
            if (movie is null)
            {
                return NotFound();
            }
            return Ok(movie);
        }
    }
}
