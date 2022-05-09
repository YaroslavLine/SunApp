using System;

namespace ServerApp.Models.ApiModels
{
    public class ApiResponseCityModel
    {
        public string CityName { get; set; }
        public DateTime? Sunrise { get; set; }
        public DateTime? Sunset { get; set; }
    }
}