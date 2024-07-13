using System.Net;

namespace BazaarOnline.Application.Requesters;


public static class TrezPanel
{
    private const string SendSMSUri = "http://www.smspanel.trez.ir/SendPatternCodeWithUrl.ashx";

    private static readonly HttpClient Client = GetClient();

    private static HttpClient GetClient()
    {
        var cli = new HttpClient();
        return cli;
    }

    public static bool SendCodeSMS(string phoneNumber, string code)
    {
        string requestUri = SendSMSUri;

        var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
        request.Headers.Add("Authorization", "Basic VGhlUG9rZXJGYWNlOiNBeGtVc2JJMzM=");

        var content = new MultipartFormDataContent();
        content.Add(new StringContent("dbc8349a-bed2-4249-9d24-e550a868b66c"), "AccessHash");
        content.Add(new StringContent("e6077597-eec1-4ce1-8f85-4ac544fec4c3"), "PatternId");
        content.Add(new StringContent(phoneNumber), "Mobile");
        content.Add(new StringContent(code), "token1");
        request.Content = content;

        var response = Client.SendAsync(request).Result;

        if (!response.IsSuccessStatusCode)
            throw new WebException($"Sending request to url has not success status code - {response.StatusCode}");

        var resultString = response.Content.ReadAsStringAsync().Result;
        var result = -1;
        if (!int.TryParse(resultString, out result))
            throw new WebException($"Error convert result to number! - Result: {result}");

        if (result < 2000)
            throw new WebException($"Result wasn't successful! - Result: {result}");

        return result >= 2000;
    }
}