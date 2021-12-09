using System.Net.Http;

namespace WmdaConnect.Shared
{
    public static class HttpResponseMessageExtensions
    {
        public static void EnsureSuccessCodeReportBody(this HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode) return;

            var body = response.Content.ReadAsStringAsync().Result;

            if (!string.IsNullOrEmpty(body)) body = "\r\n" + body;
                
            throw new HttpRequestException($"{(int)response.StatusCode}/{response.StatusCode} from {response.RequestMessage?.RequestUri}{body}");
        }
    }
}
