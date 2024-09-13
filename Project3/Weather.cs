using System;
using System.Net.Http;
using System.Runtime.InteropServices.Marshalling;
using Newtonsoft.Json.Linq;

namespace Project3
{
    public class Weather
    {
        private static string GetWeatherAPI()
        {
            string currentDir = AppDomain.CurrentDomain.BaseDirectory;
            string apiKey = string.Empty;

            //Get parent directory where weatherAPI.txt is located
            try
            {
                while (!currentDir.EndsWith("Project3"))
                    currentDir = Directory.GetParent(currentDir).FullName;
            }
            catch (DirectoryNotFoundException ex)
            {
                Debug.ExceptionLog(ex, "Directory not found!");
                return null;
            }

            //Get weatherAPI.txt
            try
            {
                using (StreamReader streamReader = new StreamReader(currentDir + "\\weatherAPI.txt"))
                    apiKey = streamReader.ReadToEnd();
            }
            catch (FileNotFoundException ex)
            {
                Debug.ExceptionLog(ex, "API Key not found!");
                return null;
            }

            return apiKey;
        }

        public static string GetCurrentWeather(string city)
        {
            HttpClient client = new HttpClient();
            string apiKey = GetWeatherAPI();

            if(apiKey == string.Empty || apiKey == null)
                throw new ArgumentNullException("API is null!");

            string cityName = city;

            string userURL = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={apiKey}&units=metric";

            string weatherResponse = string.Empty;
            try
            {
                weatherResponse = client.GetStringAsync(userURL).Result;
            }
            catch(Exception ex)
            {
                Debug.ExceptionLog(ex, ex.Message + ex.Source);
                return "Unknown error! Try to write like this:\n/weather Moscow\n/weather Baku\netc.";
            }

            var formattedResponseMain = JObject.Parse(weatherResponse);

            //Weather info
            string currentWeather = formattedResponseMain["weather"][0]["main"].ToString();
            string currentTemprature = formattedResponseMain["main"]["temp"].ToString();
            string currentFeelsLike = formattedResponseMain["main"]["feels_like"].ToString();
            string currentWindSpeed = formattedResponseMain["wind"]["speed"].ToString();
            string currentCloudiness = formattedResponseMain["clouds"]["all"].ToString();

            return $"Current weather in {cityName}:" +
                $"\nWeather: {currentWeather}" +
                "\n" +
                $"\nTemprature: {(int)Convert.ToDouble(currentTemprature)}°C" +
                $"\nFeels like: {(int)Convert.ToDouble(currentFeelsLike)}°C" +
                $"\nWind speed: {currentWindSpeed}m/s" +
                $"\nCloudiness: {currentCloudiness}%";
        }
    }
}
