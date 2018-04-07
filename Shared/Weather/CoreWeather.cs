using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class CoreWeather
    {
        public static async Task<WeatherDay> GetWeather(string nameCity, bool start)
        {
            string queryString = "https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22" + nameCity + "%22)&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";

            dynamic result = await DataService.getDataFromPogrebok(queryString).ConfigureAwait(false);

            if (start == true)
            {
                dynamic weatherOverview = result["query"]["results"]["channel"];

                if ((string)weatherOverview["description"] != "Yahoo! Weather Error")
                {
                    WeatherDay weather = new WeatherDay();

                    weather.Title = (string)weatherOverview["description"];

                    dynamic wind = weatherOverview["wind"];
                    weather.Wind = (string)wind["speed"];

                    dynamic atmosphere = weatherOverview["atmosphere"];
                    weather.Humidity = (string)atmosphere["humidity"];
                    weather.Visibility = (string)atmosphere["visibility"];
                    weather.Pressure = (string)atmosphere["pressure"];

                    dynamic astronomy = weatherOverview["astronomy"];
                    weather.Sunrise = (string)astronomy["sunrise"];
                    weather.Sunset = (string)astronomy["sunset"];

                    dynamic image = weatherOverview["image"];
                    weather.imgSource = (string)image["url"];

                    dynamic weatherType = weatherOverview["item"]["condition"];
                    weather.Temperature = Math.Round((float)((weatherType["temp"] - 32) / 1.8), 1).ToString();
                    weather.typeWeather = (string)weatherType["text"];

                    dynamic weatherOverviewDays = weatherOverview["item"]["forecast"];
                    //for (int i=0; weatherOverviewDays[i] <10; i++)
                    //{
                    //   // dynamic weatherOverviewDays = weatherOverview["item"]["forecast"][i];
                    //    weather.date[i] = (string)weatherOverviewDays["date"];
                    //    weather.day[i] = (string)weatherOverviewDays["day"];
                    //    weather.temp_high[i] = (string)weatherOverviewDays["high"];
                    //    weather.temp_low[i] = (string)weatherOverviewDays["low"];
                    //}

                    // ---- считывание данных на 10 дней вперед
                    int i = 0;
                    foreach (dynamic weat in weatherOverviewDays)
                    {
                        weather.date[i] = (string)weat["date"];
                        weather.day[i] = (string)weat["day"];
                        weather.temp_high[i] = Math.Round((float)((weat["high"] - 32) / 1.8), 1).ToString();
                        weather.temp_low[i++] = Math.Round((float)((weat["low"] - 32) / 1.8), 1).ToString();
                    }
                    return weather;
                }
                else
                {
                    return null;
                }
            }
            else return null;
        }
    }
}
