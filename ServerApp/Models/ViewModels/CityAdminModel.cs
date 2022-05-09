using System.ComponentModel.DataAnnotations;

namespace ServerApp.Models.ViewModels
{
    public class CityAdminModel
    {
        public long Id { get; set; }
        [Required]
        [Display(Name = "City Name")]
        public string CityName { get => City.Name; set => City.Name = value; }
        [Required]
        public string Latitude { get => City.Latitude; set => City.Latitude = value; }
        [Required]
        public string Longitude { get => City.Longitude; set => City.Longitude = value; }
        public City City { get; set; } = new City();
    }
}