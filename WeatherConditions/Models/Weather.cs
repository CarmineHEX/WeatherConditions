using NPOI.SS.UserModel;
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


        private Weather GetWeatherDataFromRow(IRow row)
        {
            Weather data = new Weather();

            data.Date = row.GetCell(0).ConvertDateTimeValue();
            data.Time = row.GetCell(1).ConvertTimeSpan();
            data.Temperature = row.GetCell(2).ConvertFloatValue();
            data.AirHumidity = row.GetCell(3).ConvertIntValue();
            data.DewPoint = row.GetCell(4).ConvertFloatValue();
            data.Pressure = row.GetCell(5).ConvertIntValue();
            data.WindDirection = row.GetCell(6).ConvertStringValue();
            data.SpeedWind = row.GetCell(7).ConvertIntValue();
            data.CloudCover = row.GetCell(8).ConvertIntValue();
            data.LowerCloudLimit = row.GetCell(9).ConvertIntValue();
            data.HorizontalVisibility = row.GetCell(10).ConvertIntValue();
            data.WeatherPhenomena = row.GetCell(11).ConvertStringValue();

            return data;
        }

        public List<Weather> GetListWeather(ISheet sheet)
        {
            var weatherDataList = new List<Weather>();
            int extraRow = 6;

            for (int row = extraRow; row <= sheet.LastRowNum; row++)
            {
                var currentRow = sheet.GetRow(row);

                weatherDataList.Add(GetWeatherDataFromRow(currentRow));
            }

            return weatherDataList;
        }
    }
}
