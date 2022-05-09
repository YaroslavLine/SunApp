using ServerApp.DataModels;
using ServerApp.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ServerApp.Models.ViewModels;
using log4net;
using ServerApp.Infrastructure;

namespace ServerApp.Controllers.Api
{
    public class CitiesController : ApiController
    {
        private HttpClient _httpClient = new HttpClient();
        private ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private ICityRepository _repository = new CityRepository();
        public CitiesController(ICityRepository repository)
        {
            _repository = repository;
        }
        public CitiesController()
        {

        }

        // GET api/Cities/
        // GET api/Cities/searchterm/term
        [HttpGet]
        public async Task<IEnumerable<City>> Get(string searchTerm = null)
        {
            return await _repository.GetCityList(searchTerm);
        }

        // GET api/Cities/1
        [HttpGet]
        public async Task<City> GetCity(long cityId)
        {
            return await _repository.GetCity(cityId);
        }

        // POST api/Cities/
        [HttpPost]
        public IHttpActionResult CreateCity([FromBody] CityAdminModel newCity)
        {
            if (Extensions.ArePropertiesNotNull(newCity) && newCity != null)
            {
                City city = newCity.City;
                _repository.AddCity(city);
                _log.Info("City has been saved");
                return Ok(city.Id);
            }
            else
            {
                _log.Error("Error at saving new city");
                return BadRequest();
            }
        }

        // PUT api/Cities/1
        [HttpPut]
        public async Task<IHttpActionResult> UpdateCity(long cityId, [FromBody] CityAdminModel editedCity)
        {
            City currentCity = await _repository.GetCity(cityId);
            if (Extensions.ArePropertiesNotNull(editedCity) && currentCity != null)
            {
                City resultCity = await _repository.UpdateCity(cityId, editedCity);
                _log.Info("City has been updated");
                return Ok(resultCity.Id);
            }
            else
            {
                _log.Error("Error at updating city data");
                return BadRequest();
            }
        }


        // DELETE api/Cities/5
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteCity(long cityId)
        {
            try
            {
                City deletedCity = await _repository.DeleteCity(cityId);
                return Ok(deletedCity.Id);
            }
            catch
            {
                _log.Error("Error at updating city data");
                return BadRequest();
            }
        }
        protected override void Dispose(bool disposing)
        {
            _httpClient.Dispose();
            base.Dispose(disposing);
        }
    }
}
