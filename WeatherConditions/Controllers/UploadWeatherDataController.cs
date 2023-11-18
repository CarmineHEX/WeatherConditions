using Microsoft.AspNetCore.Mvc;
using WeatherConditions.Models;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Globalization;


namespace WeatherConditions.Controllers
{
    public class UploadWeatherDataController : Controller
    {
        private readonly WeatherContext _context;

        public UploadWeatherDataController (WeatherContext context)
        {
           _context = context;
        }

        public IActionResult ViewUploadWeatherData()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> Upload(List<IFormFile> files)
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

                                try
                                {
                                    await SaveWeatherDataInBD(GetListWeather(sheet));
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

        private Weather GetWeatherDataFromRow (IRow row)
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

        private List<Weather>  GetListWeather (ISheet sheet)
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

        private async Task SaveWeatherDataInBD(List<Weather> weatherDataList)
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

