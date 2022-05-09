using Newtonsoft.Json;

namespace ServerApp.Models
{
    public class City
    {
        public long Id { get; set; }
        [JsonProperty("CityName")]
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}