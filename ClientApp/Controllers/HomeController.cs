using ClientApp.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Mvc;

namespace ClientApp.Controllers
{
    public class HomeController : Controller
    {
        private log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        HttpClient client = new HttpClient();

        public async Task<ActionResult> Index(string searchTerm = null)
        {
            try
            {
                client.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServerApp"]);
                client.DefaultRequestHeaders.Clear();

                HttpResponseMessage apiResponse = await client.GetAsync(searchTerm == null ? "Cities" : $"Cities/search/{searchTerm}");
                City[] cities = null;
                if (apiResponse.IsSuccessStatusCode)
                {
                    cities = JsonConvert.DeserializeObject<City[]>(await apiResponse.Content.ReadAsStringAsync());
                }
                return View(cities);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                return View("_Error");
            }
        }

        public async Task<ActionResult> EventInfo(long cityId, EventTime? eventTime = null)
        {
            try
            {
                if (eventTime != null)
                {
                    ViewBag.Event = eventTime.Value;
                }
                client.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServerApp"]);
                client.DefaultRequestHeaders.Clear();

                HttpResponseMessage apiResponse = await client.GetAsync($"EventCity/{cityId}/{eventTime.GetValueOrDefault()}");
                ApiResponseCityModel result = null;
                if (apiResponse.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<ApiResponseCityModel>(await apiResponse.Content.ReadAsStringAsync());
                }

                return View(result);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                return View("_Error");
            }
        }
        [HttpGet]
        public async Task<ActionResult> EditCity(long cityId = 0)
        {
            ViewBag.Title = "Edit City";

            try
            {
                if (cityId == 0)
                {
                    return View("Edit", new City());
                }
                else
                {
                    client.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServerApp"]);
                    client.DefaultRequestHeaders.Clear();
                    HttpResponseMessage apiResponse = await client.GetAsync($"Cities/{cityId}");
                    City result = null;
                    if (apiResponse.IsSuccessStatusCode)
                    {
                        result = JsonConvert.DeserializeObject<City>(await apiResponse.Content.ReadAsStringAsync());
                    }
                    return View("Edit", result);
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                return View("_Error");
            }
        }
        [HttpPost]
        public async Task<ActionResult> UpdateCity(City editedCity)
        {
            string error = null;
            try
            {
                if (ModelState.IsValid)
                {
                    client.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServerApp"]);
                    client.DefaultRequestHeaders.Clear();
                    string jsonCity = JsonConvert.SerializeObject(editedCity);
                    var httpContent = new StringContent(jsonCity, Encoding.UTF8, "application/json");

                    if (editedCity.Id == 0)
                    {
                        HttpResponseMessage apiResponse = await client.PostAsync("Cities/", httpContent);
                        if (!apiResponse.IsSuccessStatusCode)
                        {
                            error = await apiResponse.Content.ReadAsStringAsync();
                        }
                    }
                    else
                    {
                        HttpResponseMessage apiResponse = await client.PutAsync($"Cities/{editedCity.Id}", httpContent);
                        if (!apiResponse.IsSuccessStatusCode)
                        {
                            error = await apiResponse.Content.ReadAsStringAsync();
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View("Edit");
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message + " " + error);
                return View("_Error");
            }
        }
        [HttpPost]
        public async Task<ActionResult> DeleteCity(long cityId)
        {
            string error = null;
            try
            {
                client.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServerApp"]);
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage apiResponse = await client.DeleteAsync($"Cities/{cityId}");
                if (!apiResponse.IsSuccessStatusCode)
                {
                    error = await apiResponse.Content.ReadAsStringAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message + " " + error);
                return View("_Error");
            }
        }
    }
}