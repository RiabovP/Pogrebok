using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Core
    {
        public static async Task<PogrebokV1> GetPogrebokData()
        {
            string queryString = "http://37.193.0.199:1010/info.php";

            dynamic result = await DataService.getDataFromPogrebok(queryString).ConfigureAwait(false);

            dynamic pogrebokOverview = result["contents"];

            PogrebokV1 pogrebData = new PogrebokV1();

            pogrebData.street_temp_max = (string)pogrebokOverview["temp_street_max"];
            pogrebData.street_temp_min = (string)pogrebokOverview["temp_street_min"];
            pogrebData.cellar_temp = (string)pogrebokOverview["temp_cellar"];
            pogrebData.street_temp_current = (string)pogrebokOverview["temp_street"];
            pogrebData.home_temp = (string)pogrebokOverview["temp_home"];
            pogrebData.kwt_full = (string)pogrebokOverview["kwt_full"];
            pogrebData.time_power = (string)pogrebokOverview["time_power"];
            pogrebData.count_tarn = (string)pogrebokOverview["count_tarn"];
            pogrebData.price_kWt = Math.Round(((float)pogrebokOverview["kwt_full"]*2.42),2).ToString();
            pogrebData.pressure = (string)pogrebokOverview["pressure"]; 

            return pogrebData;
        }

    }
}
