using System.ComponentModel.DataAnnotations;

namespace POC_DotNET_6.Models.Dtos
{
    public class TrailUpdateDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Distance { get; set; }

        public DifficultyType Difficulty { get; set; }
        [Required]
        public int NationalParkId { get; set; }
        [Required]
        public double Elevation { get; set; }
    }
}
