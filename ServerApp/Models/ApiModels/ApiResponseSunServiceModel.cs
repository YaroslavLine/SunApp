using Newtonsoft.Json;
using System;

namespace ServerApp.Models.ApiModels
{
    public class ApiResponseSunServiceModel
    {
        [JsonProperty("results")]
        public Response Response { get; set; }
    }
    public class Response
    {
        [JsonProperty("sunrise")]
        public DateTime Sunrise { get; set; }
        [JsonProperty("sunset")]
        public DateTime Sunset { get; set; }
    }
}