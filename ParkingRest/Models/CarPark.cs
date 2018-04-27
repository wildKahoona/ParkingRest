using System.ComponentModel.DataAnnotations;

namespace ParkingRest.Models
{
    public class CarPark
    {
        public int Id { get; set; }
        [Required]
        [Range(1, 9999)]
        public int Nummer { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Strasse { get; set; }
        [Required]
        [Range(1000, 9999)]
        public int Plz { get; set; }
        [Required]
        [StringLength(100)]
        public string Ort { get; set; }
    }
}
