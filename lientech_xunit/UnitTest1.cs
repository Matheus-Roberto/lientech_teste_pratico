using lientech_xunit.Helpers;
using Microsoft.AspNetCore.Http.HttpResults;
using test_lientech.Model;
using test_lientech.Tests;

namespace lientech_xunit
{
    public class MovieTests
    {
        [Fact]
        public async void CreateMovie()
        {
            var movieId = 1;
            var name = "nomefilme";
            var director = "diretor";
            var durantion = 112;
            var movie = new Movie(name,director,durantion);

            await using var context = new MockDb().CreateDbContext();

            var result = MovieEndPoints.AddMovie(movie, context); 


            Assert.IsType<Created<Movie>>(result);
            Assert.NotNull(movie);
            Assert.NotEmpty(context.Movie);

        }
    }

   public class RoomTests
    {
        [Fact]
        public async void CreateRoom()
        {
            var roomId = 2;
            var number = "number";
            var description = "descrição";
            var movie = new Movie("nomefilme", "diretor", 112);
            movie.MovieId = 3;
            var room = new Room(number, description);
            room.RoomId = roomId;
            room.Movies.Add(movie);

            await using var context = new MockDb().RoomCreateDbContext();
            var result = RoomEndPoints.AddRoom(room, context);


            Assert.IsType<Created<Room>>(result);
            Assert.NotNull(room);
            Assert.NotEmpty(context.Room);


        }

    }
   
}
