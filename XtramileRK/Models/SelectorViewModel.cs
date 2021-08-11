using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Immutable;

namespace XtramileRK.Models
{
    public class SelectorViewModel
    {
        public string SelectedValue { get; set; }
        public string OnChange { get; set; }
        public ImmutableHashSet<SelectListItem> Data { get; set; }

        public SelectorViewModel(
            string selectedValue,
            string onChange,
            ImmutableHashSet<SelectListItem> data
        )
        {
            SelectedValue = selectedValue;
            OnChange = onChange;
            Data = data;
        }
    }
}
