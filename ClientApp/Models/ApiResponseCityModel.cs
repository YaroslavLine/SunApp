using System;

namespace ClientApp.Models
{
    public class ApiResponseCityModel
    {
        public string CityName { get; set; }
        public DateTime? Sunrise { get; set; }
        public DateTime? Sunset { get; set; }
    }
}