using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace NadamLib.Tests.TestModels
{
    public class TestDataEntityTableSeeder
    {
        public Random Random { get; set; }
        public string Text { get; set; }
        public int Records { get; set; }

        public TestDataEntityTableSeeder(int records = 100)
        {
            Random = new Random();
            Text = "Duis consectetur nibh non commodo rhoncus mauris risus dapibus dolor eu elementum nibh leo blandit massa Nunc" +
                "mollis eget nisi vel egestas Nam ultrices eu sem non hendrerit Sed in placerat turpis vitae accumsan neque Etiam" +
                "aliquet nibh ut turpis cursus ultricies dignissim velit suscipit Curabitur eleifend tempus ex vel vehicula nisi" +
                "sagittis eu Sed id mauris imperdiet tincidunt erat quis dapibus arcu Ut ut elit eu metus vestibulum vehicula vel" +
                "vitae neque Nullam finibus ante vitae euismod condimentum Duis ultrices vehicula tincidunt Morbi porta nulla nec" +
                "aliquam vulputate Sed accumsan risus eu semper sodales Praesent ultricies nisi id felis vehicula consectetur" +
                "Aliquam quis nunc nec ligula fermentum egestas Nunc augue sem ultricies sit amet vehicula id tempus vitae urna" +
                "Vestibulum sit amet tincidunt lectus Proin condimentum neque ac neque porttitor sit amet vestibulum lectus facilisis" +
                "Morbi quam neque tincidunt a aliquam in semper id lacus In congue nibh sit amet nisl pulvinar facilisis maximus in" +
                "urna Ut quis urna tincidunt auctor augue sit amet dapibus turpis Suspendisse pharetra elit at leo posuere molestie" +
                "Proin luctus justo nec justo posuere quis mollis dui ullamcorper Curabitur ac sem lacus Pellentesque bibendum nisl" +
                "mauris a placerat turpis suscipit eu Aliquam placerat tincidunt purus a cursus Donec auctor auctor turpis eu pharetra" +
                "Aliquam erat volutpat Sed vulputate metus molestie eleifend ipsum eu sodales sem Mauris tincidunt bibendum placerat" +
                "In non est magna Phasellus urna sem elementum ut felis sit amet sollicitudin hendrerit lorem In mattis lacinia urna" +
                "eget vehicula eros dignissim eu Maecenas eget nisi dolor In laoreet tellus at pharetra iaculis quam dolor sodales enim" +
                "eu elementum magna odio a massa Mauris viverra faucibus enim sit amet ultricies Maecenas nec ultrices diam eget rutrum" +
                "magna Sed tincidunt sapien vitae elementum cursus eros nunc aliquet risus eu tincidunt libero lectus non velit" +
                "Phasellus vel nisi rutrum elementum tellus ut venenatis nibh Suspendisse pulvinar ultrices diam eu aliquet sem ultricies" +
                "vitae Proin turpis orci tempus at ex ac porttitor hendrerit metus Suspendisse mattis luctus consequat Aliquam non diam" +
                "sit amet dui pretium sollicitudin ut et dolor Quisque diam ipsum pellentesque sit amet risus sit amet iaculis egestas" +
                "nulla Ut id iaculis erat vitae pulvinar quam Sed a pretium elit Ut vel eros a lacus rhoncus auctor id quis diam";

            Records = records;
        }

        public static IList<TestDataEntity> SeedTestDataEntityTable(int records = 100)
        {
            var seeder = new TestDataEntityTableSeeder(records);
            var testEntities = new List<TestDataEntity>();

            if( !seeder.TryReadFromFile(ref testEntities) )
            {
                var names = seeder.SeedNames();
                var dobs = seeder.SeedDobs();
                var colors = seeder.SeedColors();

                for (int i = 0; i < seeder.Records; i++)
                {
                    var colorIdx = seeder.GetRandomColorIdx();
                    testEntities.Add(new TestDataEntity()
                    {
                        Id = i + 1,
                        Name = names[i],
                        Dob = dobs[i],
                        ColorC = colors[colorIdx],
                        ColorE = (ColorEnum)Enum.Parse(typeof(ColorEnum), colorIdx.ToString())
                    });
                }
                seeder.SaveToFile(testEntities);
            }
            return testEntities;
        }

        public List<string> SeedNames()
        {
            var names = new List<string>();
            var strArr = Text.Split(' ');

            for (int i = 0; i < strArr.Length; i += 2)
            {

                for (int j = i + 1; j < strArr.Length; j += 3)
                {
                    names.Add($"{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(strArr[i])} {CultureInfo.CurrentCulture.TextInfo.ToTitleCase(strArr[j])}");
                }
                if (names.Count() > Records)
                    break;
            }

            return names;
        }

        public List<DateTime> SeedDobs()
        {
            var dobs = new List<DateTime>();
            var currDate = new DateTime(2000, 3, 27);
            int day = 0,
                month = 0,
                year = 0;
            for (int i = 0; i < Records; i++)
            {
                day += 2;
                month += 3;
                if( i % 3 == 0 )
                    year += 1;
                dobs.Add(currDate.AddDays(-day)
                                .AddMonths(-month)
                                .AddYears(-year));
            }

            return dobs;
        } 
        
        private List<ColorClass> SeedColors()
        {
            return new List<ColorClass>()
            {
                new ColorClass() { Id = 1, Name = "black"},
                new ColorClass() { Id = 2, Name = "white"},
                new ColorClass() { Id = 3, Name = "red"},
                new ColorClass() { Id = 4, Name = "green"},
                new ColorClass() { Id = 5, Name = "blue"},
                new ColorClass() { Id = 6, Name = "skincolor"},
                new ColorClass() { Id = 7, Name = "grey"},
                new ColorClass() { Id = 8, Name = "purple"},
            };
        }

        private int GetRandomColorIdx()
        {
            return Random.Next(0, 7);
        }

        private bool TryReadFromFile(ref List<TestDataEntity> testEntities)
        {
            try
            {
                TextReader tw = new StreamReader("../../app_data/test_entity_list.txt");
                var line = tw.ReadLine();
                while(!string.IsNullOrEmpty(line))
                {
                    var commaSeparated = line.ToString().Split(',');
                    //"{entity.Id},{entity.Name},{entity.ColorC.Id},{entity.ColorE},{entity.Dob.Date}"
                    testEntities.Add(new TestDataEntity()
                    {
                        Id = Convert.ToInt32(commaSeparated[0]),
                        Name = commaSeparated[1],
                        ColorC = new ColorClass()
                        {
                            Id = Convert.ToInt32(commaSeparated[2]),
                            Name = commaSeparated[3]
                        },
                        ColorE = (ColorEnum)Enum.Parse(typeof(ColorEnum), commaSeparated[3]),
                        Dob = Convert.ToDateTime(commaSeparated[4])
                    });
                    line = tw.ReadLine();
                }
            }
            catch(FileNotFoundException)
            {
                return false;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"{ex.Message}\n{ex?.InnerException?.Message}");
                return false;
            }

            return true;
        }

        private bool SaveToFile(List<TestDataEntity> testData)
        {
            TextWriter tw = new StreamWriter("../../app_data/test_entity_list.txt", true, Encoding.UTF8);
            try
            {
                foreach (var entity in testData)
                    tw.WriteLine($"{entity.Id},{entity.Name},{entity.ColorC.Id},{entity.ColorE},{entity.Dob.Date}");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"{ex.Message}\n{ex?.InnerException?.Message}");
                return false;
            }
            finally
            {
                tw.Close();
            }

            return true;
        }

        // number seeder
        public static IList<int> SeedNumbers()
        {
            var numbers = new List<int>(100);
            for (int i = 0; i < 100; i++)
            {
                numbers.Add(i);
            }
            return numbers;
        }

        public static IList<int> SeedNumbers3TimesEach()
        {
            var numbers = new List<int>(100);
            for (int i = 0; i < 100; i++)
            {
                numbers.Add(i);
                numbers.Add(i);
                numbers.Add(i);
            }
            return numbers;
        }

        // string seeder
        public static IList<string> SeedStrings()
        {
            return new List<string>()
            {
                "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten"
            };
        }

        public static IList<string> SeedStrings2TimesEach()
        {
            return new List<string>()
            {
                "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten",
                "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten"
            };
        }
    }
}
