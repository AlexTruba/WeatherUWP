using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherUWP.Model;

namespace WeatherUWP
{
    class WeatherApiService
    {
        public string BaseUrl => "http://localhost:10490/api/WeatherInfoApi/GetItem";
        public bool IsError { get; set; }

        public async Task<RootObject> GetInfoAsync(string cityName)
        {
            RootObject rt = null;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(BaseUrl);
                    var result = await client.GetAsync($"?city={cityName}&day=1");
                    if (result.IsSuccessStatusCode)
                    {
                        IsError = false;
                        string info = await result.Content.ReadAsStringAsync();
                        rt = JsonConvert.DeserializeObject<RootObject>(info);
                    }
                    else
                    {
                        IsError = true;
                    }
                }
                catch (Exception)
                {
                    IsError = true;
                }
            }
            return rt;
        }
    }
}
