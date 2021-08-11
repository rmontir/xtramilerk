using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using XtramileRK.Models;

namespace XtramileRK.ViewComponents
{
    [ViewComponent(Name = "CountrySelector")]
    public class CountrySelectorViewComponent : ViewComponent
    {
		public IViewComponentResult Invoke(string selectedValue, string onChange)
		{
            ImmutableHashSet<SelectListItem> items = GetItems();
            SelectorViewModel model = new SelectorViewModel(
                selectedValue: selectedValue,
                onChange: onChange,
                data: items
            );

            return View(model);
        }

		private ImmutableHashSet<SelectListItem> GetItems()
		{
            ImmutableHashSet<SelectListItem> countries = ImmutableHashSet<SelectListItem>.Empty;

			CultureInfo[] cultureInfos = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
			foreach (CultureInfo cultureInfo in cultureInfos)
            {
				RegionInfo regionInfo = new RegionInfo(cultureInfo.LCID);
				if (!countries.Any(c => c.Value == regionInfo.TwoLetterISORegionName))
                {
					countries = countries.Add(new SelectListItem(regionInfo.EnglishName, regionInfo.TwoLetterISORegionName));
                }
            }

			return countries;
		}
	}
}
