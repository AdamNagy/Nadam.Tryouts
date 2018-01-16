using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nadam.ConsoleTest.MIV
{
    public class SequenceExtension
    {
        public int Id { get; set; }
        public int SequenceId { get; set; }
        public string Description { get; set; }
        public string Directory { get; set; }
        public DateTime CreationDate { get; set; }

        public int TightsTypeId { get; set; }
        [ForeignKey("TightsTypeId")]
        public TightsType TightsType { get; set; }

        public int HighHeelId { get; set; }
        [ForeignKey("HighHeelId")]
        public HighHeel HighHeel { get; set; }


        public int PlaceId { get; set; }
        [ForeignKey("PlaceId")]
        public Place Place { get; set; }
    }
}
