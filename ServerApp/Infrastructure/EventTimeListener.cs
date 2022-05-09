using ServerApp.Models;
using ServerApp.Models.ApiModels;

namespace ServerApp.Infrastructure
{
    public class EventTimeListener
    {
        public ApiResponseCityModel GetResponseModel(string cityName, ApiResponseSunServiceModel responseModel, EventTime? eventTime)
        {
            switch (eventTime)
            {
                case EventTime.Sunrise:
                    return new ApiResponseCityModel { CityName = cityName, Sunrise = responseModel.Response.Sunrise };
                case EventTime.Sunset:
                    return new ApiResponseCityModel { CityName = cityName, Sunset = responseModel.Response.Sunset };
                case EventTime.SunriseAndSunset:
                    return new ApiResponseCityModel { CityName = cityName, Sunrise = responseModel.Response.Sunrise, Sunset = responseModel.Response.Sunset };
                default: return null;
            }
        }
    }
}