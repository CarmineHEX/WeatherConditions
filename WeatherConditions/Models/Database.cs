using Microsoft.AspNetCore.Mvc;

namespace WeatherConditions.Models
{
    public class Database : Controller
    {
        private readonly WeatherContext _context;

        public Database(WeatherContext context)
        {
            _context = context;
        }

        public async Task SaveWeatherDataInBD(List<Weather> weatherDataList)
        {
            if (!DatabaseExists())
            {
                CreateDatabase();
            }

            foreach (var weatherData in weatherDataList)
            {
                if (ModelState.IsValid)
                {
                    await _context.Weathers.AddAsync(weatherData);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "В файле есть некорректные данные.");
                }
            }

            await _context.SaveChangesAsync();
        }

        private bool DatabaseExists()
        {
            return _context.Database.CanConnect();
        }

        private void CreateDatabase()
        {
            _context.Database.EnsureCreated();
        }
    }
}
