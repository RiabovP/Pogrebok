using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Shared
{
    public class Core_Weather_api
    {
        const string cURL = "https://weather-ydn-yql.media.yahoo.com/forecastrss";
        const string cAppID = "Bms2Me7c";
        const string cConsumerKey = "dj0yJmk9TXBOOWQ0RzBvUHN4JmQ9WVdrOVFtMXpNazFsTjJNbWNHbzlNQS0tJnM9Y29uc3VtZXJzZWNyZXQmeD0yOQ--";
        const string cConsumerSecret = "898c0801e3a3a92043ae49255b2b978ace36295c";
        const string cOAuthVersion = "1.0";
        const string cOAuthSignMethod = "HMAC-SHA1";
        const string cWeatherID = "woeid=2122541";  // Novosibirsk, Russia
        const string cUnitID = "u=c";           // Metric units
        const string cFormat = "json";

        static string _get_timestamp()
        {
            TimeSpan lTS = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64(lTS.TotalSeconds).ToString();
        }  // end _get_timestamp

        static string _get_nonce()
        {
            return Convert.ToBase64String(
             new ASCIIEncoding().GetBytes(
              DateTime.Now.Ticks.ToString()
             )
            );
        }  // end _get_nonce

        static string _get_auth()
        {
            string lNonce = _get_nonce();
            string lTimes = _get_timestamp();
            string lCKey = string.Concat(cConsumerSecret, "&");
            string lSign = string.Format(  // note the sort order !!!
             "format={0}&" +
             "oauth_consumer_key={1}&" +
             "oauth_nonce={2}&" +
             "oauth_signature_method={3}&" +
             "oauth_timestamp={4}&" +
             "oauth_version={5}&" +
             "{6}&{7}",
             cFormat,
             cConsumerKey,
             lNonce,
             cOAuthSignMethod,
             lTimes,
             cOAuthVersion,
             cUnitID,
             cWeatherID
            );

            lSign = string.Concat(
             "GET&", Uri.EscapeDataString(cURL), "&", Uri.EscapeDataString(lSign)
            );

            using (var lHasher = new HMACSHA1(Encoding.ASCII.GetBytes(lCKey)))
            {
                lSign = Convert.ToBase64String(
                 lHasher.ComputeHash(Encoding.ASCII.GetBytes(lSign))
                );
            }  // end using

            return "OAuth " +
                   "oauth_consumer_key=\"" + cConsumerKey + "\", " +
                   "oauth_nonce=\"" + lNonce + "\", " +
                   "oauth_timestamp=\"" + lTimes + "\", " +
                   "oauth_signature_method=\"" + cOAuthSignMethod + "\", " +
                   "oauth_signature=\"" + lSign + "\", " +
                   "oauth_version=\"" + cOAuthVersion + "\"";

        }  // end _get_auth

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)  //Перевод секунд в дату
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public static async Task<WeatherDay> GetWeather()
        {
           
            const string lURL = cURL + "?" + cWeatherID + "&" + cUnitID + "&format=" + cFormat;

            var lClt = new WebClient();

            lClt.Headers.Set("Content-Type", "application/" + cFormat);
            lClt.Headers.Add("X-Yahoo-App-Id", cAppID);
            lClt.Headers.Add("Authorization", _get_auth());

            byte[] lDataBuffer = lClt.DownloadData(lURL);

            string lOut = Encoding.ASCII.GetString(lDataBuffer);

            dynamic data = JsonConvert.DeserializeObject(lOut);

            dynamic weatherOverview = data["current_observation"];

            if (true)
            {
                    WeatherDay weather = new WeatherDay();

                    dynamic wind = weatherOverview["wind"];
                    weather.Wind = (string)wind["speed"];

                    dynamic atmosphere = weatherOverview["atmosphere"];
                    weather.Humidity = (string)atmosphere["humidity"];
                    weather.Visibility = (string)atmosphere["visibility"];
                    weather.Pressure = Math.Round((float)((atmosphere["pressure"] * 0.750064)), 1).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                    //weather.Pressure = (string)(atmosphere["pressure"]* 0.750064);  // Перевод миллибары в мм.рт.ст

                    dynamic astronomy = weatherOverview["astronomy"];
                    weather.Sunrise = (string)astronomy["sunrise"];
                    weather.Sunset = (string)astronomy["sunset"];

                    //dynamic image = weatherOverview["image"];
                    //weather.imgSource = (string)image["url"];

                    dynamic weatherType = weatherOverview["condition"];
                    weather.Temperature = weatherType["temperature"];
                    weather.typeWeather = (string)weatherType["text"];
                    // weather.Temperature = Math.Round((float)((weatherType["temperature"] - 32) / 1.8), 1).ToString(); //перевод Фарадея в Цельсии


                    dynamic weatherOverviewDays = data["forecasts"];

                    // ---- считывание данных на 10 дней вперед
                    int i = 0;
                    DateTime dt;
                    foreach (dynamic weat in weatherOverviewDays)
                    {

                        dt=UnixTimeStampToDateTime((int)weat["date"]);
                        weather.date[i] = (string)dt.Date.ToShortDateString();
                        weather.day[i] = (string)weat["day"];
                        weather.temp_high[i] = weat["high"];
                        weather.temp_low[i++] = weat["low"];
                        //weather.temp_high[i] = Math.Round((float)((weat["high"] - 32) / 1.8), 1).ToString();
                        //weather.temp_low[i++] = Math.Round((float)((weat["low"] - 32) / 1.8), 1).ToString();
                    }
                    return weather;
                }
                else
                {
                    return null;
                }
        }
    }
}
