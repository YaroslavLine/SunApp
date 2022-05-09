using System.ComponentModel.DataAnnotations;

namespace ClientApp.Models
{
    public class City
    {
        public long Id { get; set; }
        [Required]
        [Display(Name = "City Name")]
        public string CityName { get; set; }
        [Required]
        public string Latitude { get; set; }
        [Required]
        public string Longitude { get; set; }
    }
}