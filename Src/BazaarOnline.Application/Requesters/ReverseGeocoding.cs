using BazaarOnline.Application.DTOs.RequestersDTOs.ReverseGeocodingDTOs;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;

namespace BazaarOnline.Application.Requesters;

public static class ReverseGeocoding
{
    private const string ReverseGeocodeUri =
        "https://nominatim.openstreetmap.org/reverse?format=json&lat={0}&lon={1}&zoom=5";

    private static readonly HttpClient Client = GetClient();

    private static HttpClient GetClient()
    {
        var cli = new HttpClient();
        cli.DefaultRequestHeaders.UserAgent.Clear();
        cli.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("BazaarOnline", null));
        return cli;
    }

    public static bool IsCoordinateInsideProvince(string provinceISO, double latitude, double longitude)
    {
        string requestUri = string.Format(ReverseGeocodeUri, latitude, longitude);

        var response = Client.GetAsync(requestUri).Result;
        if (response.StatusCode == HttpStatusCode.NotFound)
            return false;

        if (!response.IsSuccessStatusCode)
            throw new WebException("Sending request to url has not success status code");

        var resultString = response.Content.ReadAsStringAsync().Result;
        var result = JsonConvert.DeserializeObject<ReverseGeocodingResponseDTO>(resultString);

        if (result?.Error != null)
            throw new WebException("Request to url has error");

        if (result?.Address?.ISO3166 == null)
            throw new JsonException("Can't get ISO3166 from response body");

        return result.Address.ISO3166 == provinceISO;
    }
}