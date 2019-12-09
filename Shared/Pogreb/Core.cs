using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Core
    {
        public static async Task<PogrebokV1> GetPogrebokData()
        {
           // string queryString = "http://37.193.0.199:1010/info.php";
            string queryString = "http://37.193.0.199:1010/home2.txt";

            dynamic result = await DataService.getDataFromPogrebok(queryString).ConfigureAwait(false);

            dynamic pogrebokOverview = result["contents"];

            PogrebokV1 pogrebData = new PogrebokV1();

            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US"); //смена локализации для перевода запятых в точки в строках чисел

            pogrebData.street_temp_max = (string)pogrebokOverview["temp_street_max"];
            pogrebData.street_temp_min = (string)pogrebokOverview["temp_street_min"];
            pogrebData.cellar_temp = (string)pogrebokOverview["temp_cellar"];
            pogrebData.street_temp_current = (string)pogrebokOverview["temp_street"];
            pogrebData.home_temp = (string)pogrebokOverview["temp_home"];
            pogrebData.kwt_full = (string)pogrebokOverview["kwt_full"];
            pogrebData.time_power= Math.Round(((float)pogrebokOverview["time_power"] /60 ), 2).ToString();
            pogrebData.count_tarn = (string)pogrebokOverview["count_tarn"];
            pogrebData.price_kWt = Math.Round(((float)pogrebokOverview["kwt_full"]*2.42),2).ToString();
            pogrebData.pressure = (string)pogrebokOverview["pressure"];
            pogrebData.heating = (string)pogrebokOverview["heating"];
            pogrebData.date_hange = (string)pogrebokOverview["date_hange"];


            return pogrebData;
        }

        public static async Task<PogrebokV1> GetPogrebokData_temp()
        {
            string queryString = "http://37.193.0.199:1010/Tmax_Tmin.php?Tmax_Tmin";

            dynamic result = await DataService.getDataFromPogrebok(queryString).ConfigureAwait(false);

            dynamic pogrebokOverview = result["contents"];

            PogrebokV1 pogrebData = new PogrebokV1();

            pogrebData.street_temp_max_byDate = (string)pogrebokOverview["Tmax"];
            pogrebData.street_temp_min_byDate = (string)pogrebokOverview["Tmin"];

            return pogrebData;
        }

    }
}
