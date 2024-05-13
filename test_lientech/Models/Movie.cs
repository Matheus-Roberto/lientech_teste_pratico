using System.ComponentModel.DataAnnotations;

namespace test_lientech.Model
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }
        public string Name { get; set; }
        public string Director { get; set; }
        public int Duration { get; set; }

        public Movie( string name, string director, int duration)
        {
            Name = name;
            Director = director;
            Duration = duration;
        }
    }
}
