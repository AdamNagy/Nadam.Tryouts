using POC_DotNET_6.Models.Dto;
using System.ComponentModel.DataAnnotations;

namespace POC_DotNET_6.Models.Dtos
{
    public enum DifficultyType { Easy, Moderate, Difficult, Expert }
    public class TrailDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Distance { get; set; }

        public DifficultyType Difficulty { get; set; }
        [Required]
        public int NationalParkId { get; set; }

        public NationalParkDto NationalPark { get; set; }
        [Required]
        public double Elevation { get; set; }
    }
}
