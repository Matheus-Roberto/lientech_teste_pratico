using Microsoft.AspNetCore.Http.HttpResults;
using test_lientech.Model;

namespace test_lientech.Tests
{
    public class RoomEndPoints
    {
        public static Created<Room> AddRoom(Room room, RoomDbContext db)
        {
            db.Room.Add(room);
            db.SaveChanges();
            return TypedResults.Created($"/room/{room.RoomId}", room);
        }
    }
}
