using System.Threading.Tasks;
using XtramileRK.External.Postman.Models;

namespace XtramileRK.External.Postman
{
    public interface ICitiesService
    {
        Task<CitiesResponse> GetCitiesAsync(string country);
    }
}
