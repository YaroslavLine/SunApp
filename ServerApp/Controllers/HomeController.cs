using log4net;
using ServerApp.DataModels;
using ServerApp.Models;
using ServerApp.Models.ViewModels;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ServerApp.Controllers
{
    public class HomeController : Controller
    {
        private ILog _log;

        private ICityRepository _repository;

        public HomeController(ICityRepository repository)
        {
            _repository = repository;
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        public async Task<ActionResult> Index(string search = null)
        {
            ViewBag.Title = "Home Page";

            return View(await _repository.GetCityList(search));
        }
        [HttpGet]
        public async Task<ActionResult> EditCity(long cityId = 0)
        {
            ViewBag.Title = "Edit City";

            if (cityId == 0)
            {
                return View("Edit", new CityAdminModel());
            }
            else
            {
                City city = await _repository.GetCity(cityId);
                return View("Edit", new CityAdminModel { Id = cityId, City = city });
            }
        }
        [HttpPost]
        public async Task<ActionResult> UpdateCity(CityAdminModel editedCity)
        {
            if (ModelState.IsValid)
            {
                City city = editedCity.City;
                if (editedCity.Id == 0)
                {
                    _repository.AddCity(city);
                }
                else
                {
                    await _repository.UpdateCity(editedCity.Id, editedCity);
                }
                return RedirectToAction(nameof(Index));
            }
            _log.Error("Error at updating data from admin");
            return View("Edit");
        }

        public async Task<ActionResult> DeleteCity(long cityId)
        {
            City deletedCity = await _repository.DeleteCity(cityId);
            _log.Info($"City: {deletedCity.Name} has been deleted from admin");
            return RedirectToAction(nameof(Index));
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
