using Microsoft.AspNetCore.Mvc;
using WeatherConditions.Models;

namespace WeatherConditions.Controllers
{
    public class WeatherArchivesController : Controller
    {
        private readonly WeatherContext _context;

        public WeatherArchivesController(WeatherContext context)
        {
            _context = context;
        }

        public IActionResult ViewWeatherArchives(int? year, int? month, int page = 1)
        {

            IQueryable<Weather> query = _context.Weathers;

            if (year.HasValue)
            {
                query = query.Where(w => w.Date.HasValue && w.Date.Value.Year == year.Value);
            }

            if (month.HasValue)
            {
                query = query.Where(w => w.Date.HasValue && w.Date.Value.Month == month.Value);
            }

            var pageSize = 10;
            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var weatherData = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var viewModel = new WeatherArchivesViewModel
            {
                Year = year,
                Month = month,
                Weather = weatherData,
                PageNumber = page,
                TotalPages = totalPages
            };

            return View(viewModel);
        }
    }
}
