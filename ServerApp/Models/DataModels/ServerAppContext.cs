using ServerApp.Models;
using System.Data.Entity;

namespace ServerApp.DataModels
{
    public class ServerAppContext : DbContext
    {
        public ServerAppContext() : base("DefaultConnection")
        {

        }
        public DbSet<City> Cities { get; set; }
    }
}