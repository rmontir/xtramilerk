using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using XtramileRK.Models;

namespace XtramileRK.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        #region View

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        #endregion

        #region View Component

        [HttpGet("/components/city-selector")]
        public IActionResult CitySelectorComponent(string country, string onChange)
        {
            return ViewComponent("CitySelector", new { Country = country, OnChange = onChange });
        }

        [HttpGet("/components/weather-info")]
        public IActionResult WeatherInfoComponent(string city, string countryCode)
        {
            return ViewComponent("WeatherInfo", new { City = city, CountryCode = countryCode });
        }

        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
