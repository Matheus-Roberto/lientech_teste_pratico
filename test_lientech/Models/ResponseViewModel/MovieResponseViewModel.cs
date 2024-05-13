using AWSELOAPI.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace test_lientech.Model
{
    public class MovieResponseViewModel
    {
        public List<Movie> MovieList { get; set; }
        public Meta Meta { get; set; }
    }
}
