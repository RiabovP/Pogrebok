using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public class WeatherDay:Weather
    {
        public string[] date { get; set; }
        public string[] day { get; set; }
        public string[] temp_high { get; set; }
        public string[] temp_low { get; set; }

        public WeatherDay()
        {
            date = new string[10];
            day = new string[10];
            temp_high = new string[10];
            temp_low = new string[10];
        }
    }
}
