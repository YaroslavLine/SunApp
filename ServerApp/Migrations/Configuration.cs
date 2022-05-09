using ServerApp.Models;
using System.Data.Entity.Migrations;

namespace ServerApp.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ServerApp.DataModels.ServerAppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ServerApp.DataModels.ServerAppContext context)
        {
            context.Cities.AddOrUpdate(
                new[]
                {
                    new City {Name="Киев", Latitude="50.431533", Longitude="30.524737" },
                    new City {Name="Харьков", Latitude="49.984795", Longitude="36.235004" },
                    new City {Name="Днепр", Latitude="48.459937", Longitude="35.016826" },
                    new City {Name="Одесса", Latitude="46.463197", Longitude="30.722077" },
                    new City {Name="Николаев", Latitude="46.966728", Longitude="32.002494" },
                    new City {Name="Львов", Latitude="49.838132", Longitude="24.021099" },
                    new City {Name="Черновцы", Latitude="48.322633", Longitude="25.941462" },
                    new City {Name="Запорожье", Latitude="47.848772", Longitude="35.167104" },
                    new City {Name="Кривой Рог", Latitude="47.896548", Longitude="33.374375" },
                    new City {Name="Сумы", Latitude="50.916448", Longitude="34.795672" },
                    new City {Name="Ровно", Latitude="50.612170", Longitude="26.241762" },
                    new City {Name="Ужгород", Latitude="48.617545", Longitude="22.293711" },
                    new City {Name="Донецк", Latitude="47.994628", Longitude="37.773871" },
                    new City {Name="Житомир", Latitude="50.263696", Longitude="28.664446" },
                    new City {Name="Винница", Latitude="49.232919", Longitude="28.445942" }
                });
            context.SaveChanges();
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
