using System;
using System.Collections.Immutable;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using XtramileRK.External.Postman.Models;

namespace XtramileRK.External.Postman
{
    public class CitiesService : ICitiesService, IDisposable
    {
        private readonly HttpClient _httpClient;

        public CitiesService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://countriesnow.space/api/v0.1/countries/")
            };
        }

		public async Task<CitiesResponse> GetCitiesAsync(string country)
		{
            try
            {
				HttpResponseMessage httpResponseMessage = await _httpClient.PostAsync(new Uri($"cities", UriKind.Relative),
					content: new StringContent(JsonSerializer.Serialize(new { country }), Encoding.UTF8, "application/json")
				).ConfigureAwait(false);

				httpResponseMessage.EnsureSuccessStatusCode();

				string json = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
				CitiesResponse result = JsonSerializer.Deserialize<CitiesResponse>(json);

				return result;
			}
            catch (Exception exc)
            {
				return new CitiesResponse()
				{
					IsError = true,
					Message = exc.Message,
					Cities = ImmutableHashSet<string>.Empty
				};
            }
		}

		#region IDisposable Support

		private bool _disposedValue;

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposedValue)
			{
				if (disposing)
				{
					_httpClient.Dispose();
				}
				_disposedValue = true;
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		#endregion
	}
}
