using System.ComponentModel.DataAnnotations;

namespace test_lientech.Model
{
    public class MovieRequestViewModel
    {
        public string Name { get; set; }
        public string Director { get; set; }
        public int Duration { get; set; }
    }
}
