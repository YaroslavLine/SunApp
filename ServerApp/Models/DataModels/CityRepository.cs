using log4net;
using ServerApp.Models;
using ServerApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApp.DataModels
{
    public class CityRepository : ICityRepository
    {
        private ServerAppContext _dbContext = new ServerAppContext();
        private ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public City AddCity(City newCity)
        {
            try
            {
                _dbContext.Cities.Add(newCity);
                _dbContext.SaveChanges();
                return newCity;
            }
            catch (Exception ex)
            {
                _log.Error("Internal error. " + ex.Message);
                return null;
            }
        }

        public async Task<City> DeleteCity(long cityId)
        {
            try
            {
                City city = await _dbContext.Cities.FindAsync(cityId);
                _dbContext.Cities.Remove(city);
                await _dbContext.SaveChangesAsync();
                return city;
            }
            catch (Exception ex)
            {
                _log.Error("Internal error. " + ex.Message);
                return null;
            }
        }

        public async Task<City> GetCity(long cityId)
        {
            try
            {
                return await _dbContext.Cities.FindAsync(cityId);
            }
            catch (Exception ex)
            {
                _log.Error("Internal error. " + ex.Message);
                return null;
            }
        }

        public async Task<IEnumerable<City>> GetCityList(string searchTerm = null)
        {
            try
            {
                IQueryable<City> query = _dbContext.Cities;
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query = query.Where(c => c.Name.Contains(searchTerm));
                }
                return await query.ToArrayAsync();
            }
            catch (Exception ex)
            {
                _log.Error("Internal error. " + ex.Message);
                return null;
            }
        }

        public async Task<City> UpdateCity(long cityId, CityAdminModel editedCity)
        {
            try
            {
                City currentCity = await _dbContext.Cities.FindAsync(cityId);
                currentCity.Name = editedCity.CityName;
                currentCity.Latitude = editedCity.Latitude;
                currentCity.Longitude = editedCity.Longitude;
                await _dbContext.SaveChangesAsync();
                return currentCity;
            }
            catch (Exception ex)
            {
                _log.Error("Internal error. " + ex.Message);
                return null;
            }
        }
    }
}