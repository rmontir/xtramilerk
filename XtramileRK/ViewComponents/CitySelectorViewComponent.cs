using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using XtramileRK.External.Postman;
using XtramileRK.External.Postman.Models;
using XtramileRK.Models;

namespace XtramileRK.ViewComponents
{
    [ViewComponent(Name = "CitySelector")]
    public class CitySelectorViewComponent : ViewComponent
    {
        private readonly ICitiesService _citiesService;

        public CitySelectorViewComponent(
            ICitiesService citiesService
        )
        {
            _citiesService = citiesService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string country, string onChange)
        {
            ImmutableHashSet<SelectListItem> items = await GetItemsAsync(country).ConfigureAwait(false);
            SelectorViewModel model = new SelectorViewModel(
                selectedValue: string.Empty,
                onChange: onChange,
                data: items
            );

            return View(model);
        }

        private async Task<ImmutableHashSet<SelectListItem>> GetItemsAsync(string country)
        {
            ImmutableHashSet<SelectListItem> cities = ImmutableHashSet<SelectListItem>.Empty;

            CitiesResponse response = await _citiesService.GetCitiesAsync(country).ConfigureAwait(false);
            foreach (string city in response.Cities)
            {
                if (!cities.Any(c => c.Value == city))
                {
                    cities = cities.Add(new SelectListItem(city, city));
                }
            }

            return cities;
        }
    }
}
