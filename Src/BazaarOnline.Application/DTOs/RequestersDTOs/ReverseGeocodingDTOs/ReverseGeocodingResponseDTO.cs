using Newtonsoft.Json;

namespace BazaarOnline.Application.DTOs.RequestersDTOs.ReverseGeocodingDTOs;

public class ReverseGeocodingResponseDTO
{
    [JsonProperty("error")] public object? Error { get; set; }

    [JsonProperty("address")] public ReverseGeocodingAddressDTO Address { get; set; }
}

public class ReverseGeocodingAddressDTO
{
    [JsonProperty("ISO3166-2-lvl4")] public string ISO3166 { get; set; }

    [JsonProperty("country_code")] public string CountryCode { get; set; }
}