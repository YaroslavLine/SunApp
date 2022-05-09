using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerApp.Controllers.Api;
using ServerApp.DataModels;
using ServerApp.Models;
using ServerApp.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace ServerApp.Tests
{
    [TestClass]
    public class ApiCitiesTests
    {
        public class FakeCityRepositiory : ICityRepository
        {
            public City AddCity(City newCity)
            {
                bool result = Extensions.ArePropertiesNotNull<City>(newCity);
                if (result)
                {
                    newCity.Id = 0;
                    return newCity;
                }
                else return null;
            }

            public async Task<City> DeleteCity(long cityId)
            {
                var list = await GetCityList();
                return list.FirstOrDefault(c => c.Id == cityId);
            }

            public async Task<City> GetCity(long cityId)
            {
                var list = await GetCityList();
                return list.FirstOrDefault(c => c.Id == cityId);
            }

            public Task<IEnumerable<City>> GetCityList(string searchTerm = null)
            {
                IEnumerable<City> cities = new[]
               {
                    new City {Id=1,Name="Киев", Latitude="50.431533", Longitude="30.524737" },
                    new City {Id=2,Name="Харьков", Latitude="49.984795", Longitude="36.235004" },
                    new City {Id=3,Name="Днепр", Latitude="48.459937", Longitude="35.016826" },
                    new City {Id=4,Name="Одесса", Latitude="46.463197", Longitude="30.722077" },
                    new City {Id=5,Name="Николаев", Latitude="46.966728", Longitude="32.002494" },
                    new City {Id=6,Name="Львов", Latitude="49.838132", Longitude="24.021099" },
                    new City {Id=7,Name="Черновцы", Latitude="48.322633", Longitude="25.941462" },
                    new City {Id=8,Name="Запорожье", Latitude="47.848772", Longitude="35.167104" },
                    new City {Id=9,Name="Кривой Рог", Latitude="47.896548", Longitude="33.374375" },
                    new City {Id=10,Name="Сумы", Latitude="50.916448", Longitude="34.795672" },
                    new City {Id=11,Name="Ровно", Latitude="50.612170", Longitude="26.241762" },
                    new City {Id=12,Name="Ужгород", Latitude="48.617545", Longitude="22.293711" },
                    new City {Id=13,Name="Донецк", Latitude="47.994628", Longitude="37.773871" },
                    new City {Id=14,Name="Житомир", Latitude="50.263696", Longitude="28.664446" },
                    new City {Id=15,Name="Винница", Latitude="49.232919", Longitude="28.445942" }
                };
                return Task.FromResult(cities);

            }

            public async Task<City> UpdateCity(long cityId, CityAdminModel newCity)
            {
                var list = await GetCityList();
                City city = list.FirstOrDefault(c => c.Id == cityId);
                if (city != null && Extensions.ArePropertiesNotNull<City>(newCity.City))
                {
                    city = newCity.City;
                    city.Id = cityId;
                    return city;
                }
                return null;
            }
        }

        private CitiesController controller;
        private IHttpActionResult actionResult;
        ICityRepository fakeRepository = new FakeCityRepositiory();

        [TestInitialize]
        public void SetupContext()
        {
            controller = new CitiesController(fakeRepository);
        }

        [TestMethod]
        public void MethodCreateCityResultBadRequest()
        {
            actionResult = controller.CreateCity(new CityAdminModel { Latitude = "TestLatitude", Longitude = "Longitude" });
            Assert.IsInstanceOfType(actionResult,typeof(BadRequestResult));
        }
        [TestMethod]
        public void MethodCreateCityResultOk()
        {
            actionResult = controller.CreateCity(new CityAdminModel { CityName = "test", Latitude = "TestLatitude", Longitude = "Longitude" });
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<long>));
        }
        [TestMethod]
        public async Task MethodUpdateCityResultOk()
        {
            actionResult = await controller.UpdateCity(12, new CityAdminModel { CityName = "TestName2", Latitude = "TestLatitude2", Longitude = "Longitude2" });
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<long>));
        }
        [TestMethod]
        public async Task MethodUpdateCityResultBadRequest()
        {
            actionResult = await controller.UpdateCity(-1, new CityAdminModel { CityName = "TestName2", Latitude = "TestLatitude2", Longitude = "Longitude2" });
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }
        [TestMethod]
        public async Task GetCitiesMethodSearchTermResultNotNull()
        {
            IEnumerable<City> result = await controller.Get("Киев");
            Assert.IsTrue(result.Count() > 0);
        }
        [TestMethod]
        public async Task GetCitiesMethodNotNull()
        {
            IEnumerable<City> result = await controller.Get();
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public async Task GetMethodResultIsNull()
        {
            City result = await controller.GetCity(999999999);
            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task DeleteCityBadRequest()
        {
            actionResult = await controller.DeleteCity(99999);
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }
    }
}
