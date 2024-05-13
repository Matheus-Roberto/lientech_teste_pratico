using System.ComponentModel.DataAnnotations;

namespace test_lientech.Model
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public List<Movie> Movies { get; set; }

        public Room(string number, string description)
        {
            Number = number;
            Description = description;
            Movies = new List<Movie>();
        }
    }
}
