
namespace WeatherConditions.Models
{
    public class WeatherArchivesViewModel
    {
        public int? Year { get; set; }
        public int? Month { get; set; }
        public List<Weather>? Weather { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
    }
}
