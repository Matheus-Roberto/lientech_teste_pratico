using AWSELOAPI.ViewModel;
using System.Data;
using test_lientech.Data;
using test_lientech.Model;

namespace test_lientech.Service
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ApiDbContext _apiDbContext;

        public RoomRepository(ApiDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext ?? throw new ArgumentNullException(nameof(apiDbContext));
        }

        public async Task<Room> Create(RoomRequestViewModel roomViewModel)
        {
            var room = new Room(roomViewModel.Number,roomViewModel.Description);
                _apiDbContext.Room.Add(room);
            await _apiDbContext.SaveChangesAsync();
            return room;
        }

        public async Task<Room> DeleteById(int roomId)
        {
            var room = _apiDbContext.Room.Find(roomId);
            _apiDbContext.Entry(room).Collection(c => c.Movies).Load();
            foreach (var movie in room.Movies)
            {
                _apiDbContext.Remove(movie);
            }
            _apiDbContext.Remove(room);
            await _apiDbContext.SaveChangesAsync();
            return room;
          
        }

        public Room? GetById(int roomId)
        {
            Room room = _apiDbContext.Room.Find(roomId);
            _apiDbContext.Entry(room).Collection(c => c.Movies).Load();
            return room;
        }

        public RoomResponseViewModel GetAll(int pageNumber, int pageQuantity)
        {
            RoomResponseViewModel responseViewModel = new RoomResponseViewModel();
            responseViewModel.RoomList = _apiDbContext.Room.Skip((pageNumber - 1) * pageQuantity).Take(pageQuantity).ToList();
            foreach (var room in responseViewModel.RoomList)
            {
                _apiDbContext.Entry(room).Collection(c => c.Movies).Load();
            }
            if (!responseViewModel.RoomList.Any())
            {
                throw new DBConcurrencyException("erro ao acessar o banco de dados, não foi possível pegar a lista de Vehicle");
            }
            responseViewModel.Meta = new Meta();
            responseViewModel.Meta.Total = _apiDbContext.Room.Count();
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

        public async Task<Room> Update(Room room)
        {
            _apiDbContext.Update(room);
            await _apiDbContext.SaveChangesAsync();
            return room;
        }

        public async Task<Room> Attach(int roomId, int movieId)
        {
            Room room = _apiDbContext.Room.Find(roomId);
            Movie movie = _apiDbContext.Movie.Find(movieId);
            if (room == null|| movie==null)
            {
                return null;
            }
            room.Movies.Add(movie);
            _apiDbContext.Update(room);
            await _apiDbContext.SaveChangesAsync();
            return room;
        }
        public async Task<Room> Detach(int roomId, int movieId)
        {
            Room room = _apiDbContext.Room.Find(roomId);
            Movie movie = _apiDbContext.Movie.Find(movieId);
            if (room == null || movie == null)
            {
                return null;
            }
            room.Movies.Remove(movie);
            _apiDbContext.Update(room);
            await _apiDbContext.SaveChangesAsync();
            return room;
        }
    }
}
