using Microsoft.AspNetCore.Mvc;
using WeatherConditions.Models;
using Microsoft.EntityFrameworkCore;
using NPOI.XSSF.UserModel;


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
                try
                {
                    if (file.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            file.CopyTo(stream);
                            stream.Position = 0;
                            using (var package = new XSSFWorkbook(stream))
                            {
                                var sheet = package.GetSheetAt(0);
                                Weather weather = new Weather();

                                try
                                {
                                    await db.SaveWeatherDataInBD(weather.GetListWeather(sheet));
                                }
                                catch (DbUpdateException)
                                {
                                    ModelState.AddModelError(string.Empty, "Произошла ошибка при сохранении данных в бд.");
                                }
                            }
                        }
                    }
                }

                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = $"Не удалось считать файл {file.FileName}: {ex.Message}";
                    return View("Error");
                }
            }
            return new RedirectResult("http://localhost:5138/Home");
        }
    }
}

