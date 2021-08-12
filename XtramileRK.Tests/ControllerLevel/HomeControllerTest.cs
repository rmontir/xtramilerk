using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using XtramileRK.Controllers;
using XtramileRK.External.OpenWeather;
using XtramileRK.External.OpenWeather.Models;
using XtramileRK.Tests.Fakes;
using XtramileRK.ViewComponents;

namespace XtramileRK.Tests.ControllerLevel
{
    [TestClass]
    public class HomeControllerTest
    {
        private readonly IWeatherService _weatherService;
        //private readonly IServiceProvider _serviceProvider;
        //private readonly IConfiguration _configuration;

        public HomeControllerTest()
        {
            //_configuration = new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();
            //ServiceCollection serviceCollection = new ServiceCollection();
            //serviceCollection.AddOpenWeatherServices(options =>
            //{
            //    options.ApiKey = _configuration["OpenWeather:ApiKey"];
            //});
            //_serviceProvider = serviceCollection.BuildServiceProvider();

            //_weatherService = _serviceProvider.GetRequiredService<IWeatherService>();

            _weatherService = new FakeWeatherService("1234567");
        }

        //[TestMethod]
        //public void Weather_Info_Component_Return_View_Component_When_Parameter_Is_Valid()
        //{
        //    HomeController homeController = new HomeController();
        //    ViewComponentResult weatherInfoComponent = homeController.WeatherInfoComponent("Abbey", "CA") as ViewComponentResult;

        //    Assert.IsNotNull(weatherInfoComponent);
        //    Assert.AreEqual("WeatherInfo", weatherInfoComponent.ViewComponentName);
        //}

        //[TestMethod]
        //public void Weather_Info_Component_Return_View_Component_When_Parameter_Is_Empty()
        //{
        //    HomeController homeController = new HomeController();
        //    ViewComponentResult weatherInfoComponent = homeController.WeatherInfoComponent("", "") as ViewComponentResult;

        //    Assert.IsNotNull(weatherInfoComponent);
        //    Assert.AreEqual("WeatherInfo", weatherInfoComponent.ViewComponentName);
        //}

        [TestMethod]
        public async Task Weather_Info_Component_Invoke_With_Data_When_Parameter_Is_Valid()
        {
            DefaultHttpContext httpContext = new DefaultHttpContext();
            ViewContext viewContext = new ViewContext
            {
                HttpContext = httpContext
            };
            ViewComponentContext viewComponentContext = new ViewComponentContext
            {
                ViewContext = viewContext
            };

            WeatherInfoViewComponent viewComponent = new WeatherInfoViewComponent(_weatherService)
            {
                ViewComponentContext = viewComponentContext
            };

            ViewViewComponentResult viewComponentResult = await viewComponent.InvokeAsync("London", "GB").ConfigureAwait(false) as ViewViewComponentResult;
            WeatherResponse model = viewComponentResult.ViewData.Model as WeatherResponse;

            Assert.IsFalse(model.IsError);
            Assert.IsNotNull(model.Data);
        }

        [TestMethod]
        public async Task Weather_Info_Component_Invoke_With_Error_Data_When_Parameter_Is_Invalid()
        {
            DefaultHttpContext httpContext = new DefaultHttpContext();
            ViewContext viewContext = new ViewContext
            {
                HttpContext = httpContext
            };
            ViewComponentContext viewComponentContext = new ViewComponentContext
            {
                ViewContext = viewContext
            };

            WeatherInfoViewComponent viewComponent = new WeatherInfoViewComponent(_weatherService)
            {
                ViewComponentContext = viewComponentContext
            };

            ViewViewComponentResult viewComponentResult = await viewComponent.InvokeAsync("100 Mile House", "CA").ConfigureAwait(false) as ViewViewComponentResult;
            WeatherResponse model = viewComponentResult.ViewData.Model as WeatherResponse;

            Assert.IsTrue(model.IsError);
            Assert.IsNull(model.Data);
        }

        [TestMethod]
        public async Task Weather_Info_Component_Invoke_With_No_Data_When_Parameter_Is_Empty()
        {
            DefaultHttpContext httpContext = new DefaultHttpContext();
            ViewContext viewContext = new ViewContext();
            viewContext.HttpContext = httpContext;
            ViewComponentContext viewComponentContext = new ViewComponentContext();
            viewComponentContext.ViewContext = viewContext;

            WeatherInfoViewComponent viewComponent = new WeatherInfoViewComponent(_weatherService);
            viewComponent.ViewComponentContext = viewComponentContext;

            ViewViewComponentResult viewComponentResult = await viewComponent.InvokeAsync("", "").ConfigureAwait(false) as ViewViewComponentResult;
            Assert.IsNull(viewComponentResult.ViewData.Model);
        }
    }
}
