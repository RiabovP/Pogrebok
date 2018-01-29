using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace Shared
{
    public class DataService
    {
        public static async Task<dynamic> getDataFromPogrebok (string queryString)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(queryString);

            var response = await request.GetResponseAsync().ConfigureAwait(false);

            var stream = response.GetResponseStream();

            var streamReader = new StreamReader(stream);
            string responseText = streamReader.ReadToEnd();
            //string responseText1 = '"{\"contents\":{\"temp_street_max\":\"-6.44\"},{\"temp_cellar\":\"2.62\"},{\"temp_street\":\"-23.12\"}\n{\"temp_home\":\"18.74\"},{\"kwt_full\":\"133.40\"},{\"time_power\":\"15888.90\"}\n{\"count_tarn\":\"0\"}}"';

            dynamic data = JsonConvert.DeserializeObject(responseText);

            return data;
        }
    }
}
