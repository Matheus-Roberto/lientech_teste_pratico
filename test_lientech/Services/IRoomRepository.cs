using test_lientech.Model;

namespace test_lientech.Service
{
    public interface IRoomRepository
    {

        Task<Room> Create(RoomRequestViewModel roomViewModel);
        Task<Room> Update(Room room);
        Task<Room> DeleteById(int roomId);
        Room? GetById(int roomId);
        RoomResponseViewModel GetAll(int pageNumber, int pageQuantity);
        Task<Room> Attach(int roomId, int movieId);
        Task<Room> Detach(int roomId, int movieId);
    }
}