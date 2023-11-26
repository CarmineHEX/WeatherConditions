using Microsoft.AspNetCore.Mvc;
using WeatherConditions.Models;
using Microsoft.EntityFrameworkCore;
using NPOI.XSSF.UserModel;
using System.Linq;


namespace WeatherConditions.Controllers
{
    public class UploadWeatherDataController : Controller
    {
        private readonly Database db;

        public UploadWeatherDataController(WeatherContext context)
        {
            db = new Database(context);
        }

        public IActionResult ViewUploadWeatherData()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(List<IFormFile> files)
        {
            foreach (var file in files)
            {

                if (!(file.Length > 0) || !CheckValidFileExtension(file.FileName))
                {
                    ViewBag.ErrorMessage = $"Не удалось считать файл {file.FileName}. Проверьте содержит ли файл данные и соответствует ли его разрешение .xls или .xlsx";
                    return View("Error");
                }

                using var stream = new MemoryStream();
                file.CopyTo(stream);
                stream.Position = 0;

                using var package = new XSSFWorkbook(stream);
                var sheet = package.GetSheetAt(0);
                Weather weather = new();

                try
                {
                    await db.SaveWeatherDataInBD(weather.GetListWeather(sheet));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError(string.Empty, "Произошла ошибка при сохранении данных в бд.");
                }
            }
            return new RedirectResult("http://localhost:5138/Home");
        }

        private bool CheckValidFileExtension(string fileName)
        {
            string[] allowedExtensions = new[] { ".xls", ".xlsx" };
            string? fileExtension = Path.GetExtension(fileName).ToLower();

            return allowedExtensions.Contains(fileExtension);
        }
    }
}

