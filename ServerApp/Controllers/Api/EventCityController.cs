using log4net;
using Newtonsoft.Json;
using ServerApp.DataModels;
using ServerApp.Infrastructure;
using ServerApp.Models;
using ServerApp.Models.ApiModels;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Http;

namespace ServerApp.Controllers.Api
{
    public class EventCityController : ApiController
    {
        private ICityRepository _repository = new CityRepository();
        private HttpClient _httpClient = new HttpClient();
        private EventTimeListener _listener = new EventTimeListener();
        private ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // GET api/EventCityController/5
        [HttpGet]
        public async Task<IHttpActionResult> Get(long cityId, EventTime? eventTime = null)
        {
            try
            {
                City city = await _repository.GetCity(cityId);

                if (city != null)
                {
                    _httpClient.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiSunriseSunset"]);
                    _httpClient.DefaultRequestHeaders.Clear();

                    HttpResponseMessage apiResponse = await _httpClient.GetAsync($"?lat={city.Latitude}&lng=-{city.Longitude}");
                    if (apiResponse.IsSuccessStatusCode)
                    {
                        ApiResponseSunServiceModel responseModel = JsonConvert.DeserializeObject<ApiResponseSunServiceModel>(await apiResponse.Content.ReadAsStringAsync());
                        ApiResponseCityModel result = _listener.GetResponseModel(city.Name, responseModel, eventTime);
                        if (result != null)
                        {
                            return Ok(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error at getting city events. " + ex.Message);
            }
            return BadRequest();
        }
    }
}