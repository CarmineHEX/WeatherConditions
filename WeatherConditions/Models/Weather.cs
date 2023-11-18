using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherConditions.Models
{
    public class Weather
    {
        public int Id { get; set; }
        [Column(TypeName = "Date")]
        public DateTime? Date { get; set; }
        [Column(TypeName = "Time")]
        public TimeSpan? Time { get; set; }
        public float? Temperature { get; set; }
        public int? AirHumidity { get; set; }
        public float? DewPoint { get; set; }
        public int? Pressure { get; set; }
        public string? WindDirection { get; set; }
        public int? SpeedWind { get; set; }
        public int? CloudCover { get; set; }
        public int? LowerCloudLimit { get; set; }
        public int? HorizontalVisibility { get; set; }
        public string? WeatherPhenomena { get; set; }
    }
}
