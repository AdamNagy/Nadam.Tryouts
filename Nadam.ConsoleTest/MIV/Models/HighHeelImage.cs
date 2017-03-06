using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nadam.ConsoleTest.MIV
{
    public class HighHeelImage
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int HighHeelId { get; set; }
        [ForeignKey("HighHeelId")]
        public HighHeel HighHeel { get; set; }
    }
}
