using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ApiCenter
{
    public class WeatherForecast
    {
        [Required]
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }

        [DefaultValue(true)]
        public bool IsSunnyDay { get; set; }
    }
}