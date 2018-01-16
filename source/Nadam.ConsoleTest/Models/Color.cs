using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nadam.ConsoleTest.Models
{
    public class Color
    {
        [Key]
        public int Id { get; set; }
        public string ColorName { get; set; }

        public static IList<Color> SeedBaseColors()
        {
            return new List<Color>()
            {
                new Color(){
                    Id = 1,
                    ColorName = "Black"
                },
                new Color(){
                    Id = 2,
                    ColorName = "SkinColor"
                },
                new Color(){
                    Id = 3,
                    ColorName = "White"
                },
                new Color(){
                    Id = 4,
                    ColorName = "Green"
                },
                new Color(){
                    Id = 5,
                    ColorName = "Blue"
                },
                new Color(){
                    Id = 6,
                    ColorName = "Red"
                }
                //TODO: extend
            };
        }
    }
}
