using ServerApp.Models;
using ServerApp.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerApp.DataModels
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetCityList(string searchTerm = null);
        Task<City> GetCity(long cityId);
        City AddCity(City newCity);
        Task<City> UpdateCity(long cityId, CityAdminModel newCity);
        Task<City> DeleteCity(long cityId);
    }
}