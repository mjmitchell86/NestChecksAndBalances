using NestChecksAndBalances.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace NestChecksAndBalances.Integrations
{
    public interface INestAPI
    {
        NestObject GetCurrentHouseConditions(string nestToken, string thermostatId);
    }

    public class NestAPI : INestAPI
    {
        public NestObject GetCurrentHouseConditions(string nestToken, string thermostatId)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri("https://developer-api.nest.com/devices/thermostats/" + thermostatId),
                Method = HttpMethod.Get
            };

            request.Headers.Add("Authorization", "Bearer " + nestToken);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var result = client.SendAsync(request).Result;
            return JsonConvert.DeserializeObject<NestObject>(result.Content.ReadAsStringAsync().Result);        
        }
    }
}