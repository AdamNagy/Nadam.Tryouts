using System.Collections.Generic;
using System.Linq;

namespace Nadam.ConsoleTest.Models
{
    public class ColorHelper
    {
        public IList<Color> Beckbone { get; set; }
        public ColorHelper()
        {
            Beckbone = Color.SeedBaseColors().ToList();
        }

        public Color this[string name]
        {
            get
            {
                return Beckbone.SingleOrDefault(p => p.ColorName.Equals(name));
            }
        }
    }
}
