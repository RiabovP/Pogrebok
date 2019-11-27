using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Shared.Weather
{
    class Core_Weather_api
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
            string retVal;
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

        //public static void Main(string[] args)
        //{

        //    const string lURL = cURL + "?" + cWeatherID + "&" + cUnitID + "&format=" + cFormat;

        //    var lClt = new WebClient();

        //    lClt.Headers.Set("Content-Type", "application/" + cFormat);
        //    lClt.Headers.Add("X-Yahoo-App-Id", cAppID);
        //    lClt.Headers.Add("Authorization", _get_auth());

        //    Console.WriteLine("Downloading Yahoo weather report . . .");

        //    byte[] lDataBuffer = lClt.DownloadData(lURL);

        //    string lOut = Encoding.ASCII.GetString(lDataBuffer);

        //    Console.WriteLine(lOut);

        //    Console.Write("Press any key to continue . . . ");
        //    Console.ReadKey(true);

        //}  // end Main

        public static async Task<WeatherDay> GetWeather(string nameCity, bool start)
        {
            //string queryString = "https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22" + nameCity + "%22)&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";

            const string lURL = cURL + "?" + cWeatherID + "&" + cUnitID + "&format=" + cFormat;

            var lClt = new WebClient();

            lClt.Headers.Set("Content-Type", "application/" + cFormat);
            lClt.Headers.Add("X-Yahoo-App-Id", cAppID);
            lClt.Headers.Add("Authorization", _get_auth());

            byte[] lDataBuffer = lClt.DownloadData(lURL);

            string lOut = Encoding.ASCII.GetString(lDataBuffer);


            dynamic result = await DataService.getDataFromPogrebok(lURL).ConfigureAwait(false);

            if (start == true)
            {
                dynamic weatherOverview = result["current_observation"];

                if (true)
                {
                    WeatherDay weather = new WeatherDay();

                    weather.Title = (string)weatherOverview["current_observation"];

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
