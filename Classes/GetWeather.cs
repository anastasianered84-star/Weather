// Файл: Classes/GetWeather.cs (обновленная версия)
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Weather.Models;

namespace Weather.Classes
{
    public class GetWeather
    {
        public static string WeatherUrl = "https://api.weather.yandex.ru/v2/forecast";
        public static string WeatherKey = "demo_yandex_weather_api_key_ca6d09349ba0";

        public static async Task<DataResponse> GetByCoordinates(float lat, float lon)
        {
            DataResponse dataResponse = null;
            string url = $"{WeatherUrl}?lat={lat}&lon={lon}".Replace(",", ".");

            using (HttpClient client = new HttpClient())
            {
                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url))
                {
                    request.Headers.Add("X-Yandex-Weather-Key", WeatherKey);

                    using (var response = await client.SendAsync(request))
                    {
                        string contentResponse = await response.Content.ReadAsStringAsync();
                        dataResponse = JsonConvert.DeserializeObject<DataResponse>(contentResponse);
                    }
                }
            }
            return dataResponse;
        }

        public static async Task<DataResponse> GetByCityName(string cityName)
        {
            var coordinates = await Geocoder.GetCoordinates(cityName);

            if (coordinates.lat == 0 && coordinates.lon == 0)
            {
                coordinates = (55.7558f, 37.6173f); 
            }

            return await GetByCoordinates(coordinates.lat, coordinates.lon);
        }
    }
}