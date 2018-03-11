//using Nadam.ConsoleTest.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace Nadam.ConsoleTest
//{
//    public class JsonDbTestConsole
//    {
//        public static void TestRunner()
//        {
//            var program = new JsonDbTestConsole();
//            //var list = program.SeedImages();
//            //var owners = program.SeedOwners().ToArray();

//            //var filtered = list.FilterByEquality(Filter.ColorEnum.ToString(), ColorEnum.Black);
//            //filtered = list.FilterBy(Filter.ColorEnum.ToString(), ColorEnum.Black.ToString(), (s, s1) => true);
//            //filtered = list.FilterByGreaterThan(Filter.Rating.ToString(), "3");
//            //filtered = list.FilterByEquality(Filter.User.ToString(), list.First().User);

//            //var filtered2 = list;

//            //var context = new TestJsonDbContext();
//            //context.Images = list;
//            //context.Users = program.SeedOwners();
//            //context.Colors = Color.SeedBaseColors();
//            //context.SaveChanges();
//            //program.DbState(ref context);


//            //var context2 = new TestJsonDbContext();
//            //program.DbState(ref context2);

//            //var ow = context2.Users.Single(p => p.Id == 1);
//            //var col = context2.Colors.Single(p => p.ColorName.Equals("Green"));

//            //context2.Images.Add(new Image()
//            //{
//            //    User = ow,
//            //    Color = col,
//            //    Rating = 3,
//            //    Title = "myimage_0001.jpg"
//            //});
//            //context2.Images.Add(new Image()
//            //{
//            //    User = ow,
//            //    Color = col,
//            //    Rating = 3,
//            //    Title = "myimage_0002.jpg"
//            //});
//            //context2.Images.Add(new Image()
//            //{
//            //    User = ow,
//            //    Color = col,
//            //    Rating = 3,
//            //    Title = "myimage_0003.jpg"
//            //});
//            //context2.SaveChanges();

//            //program.DbState(ref context2);

//            var ret = program.MyMethod(p => p.Id);

//            Console.WriteLine("Done!");
//            Console.ReadKey();
//        }

//        private T MyMethod<T>(Func<Color, T> selector)
//        {
//            Color obj = new Color()
//            {
//                ColorName = "red",
//                Id = 3
//            };
//            return selector(obj);
//        }

//        private IList<Image> SeedImages()
//        {
//            var colorEnum = new ColorHelper();
//            var owners = SeedOwners().ToArray();
//            return new List<Image>()
//            {
//                new Image()
//                {
//                    Id = 1,
//                    User = owners[0],
//                    Color = colorEnum["Black"],
//                    Rating = 2,
//                    Title = "First Image"
//                },
//                new Image()
//                {
//                    Id = 2,
//                    User = owners[1],
//                    Color = colorEnum["Green"],
//                    Rating = 4,
//                    Title = "Second Image"
//                },
//                new Image()
//                {
//                    Id = 3,
//                    User = owners[2],
//                    Color = colorEnum["SkinColor"],
//                    Rating = 2,
//                    Title = "Third Image"
//                },
//                new Image()
//                {
//                    Id = 4,
//                    User = owners[0],
//                    Color = colorEnum["Black"],
//                    Rating = 2,
//                    Title = "First Image"
//                },
//                new Image()
//                {
//                    Id = 5,
//                    User = owners[1],
//                    Color = colorEnum["Green"],
//                    Rating = 4,
//                    Title = "Second Image"
//                }
//            };
//        }

//        private IList<User> SeedOwners()
//        {
//            return new List<User>()
//            {
//                new User()
//                {
//                    Id = 1,
//                    Name = "Owner1",
//                    Rank = 1,
//                    RegistrationDate = DateTime.Now.AddDays(-125)
//                },
//                new User()
//                {
//                    Id = 2,
//                    Name = "Owner2",
//                    Rank = 3,
//                    RegistrationDate = DateTime.Now.AddDays(-355)
//                },
//                new User()
//                {
//                    Id = 3,
//                    Name = "Owner3",
//                    Rank = 3,
//                    RegistrationDate = DateTime.Now.AddDays(-15)
//                }
//            };
//        }

//        private void DbState(ref TestJsonDbContext context)
//        {
//            Console.WriteLine("Db tables stat:");
//            Console.WriteLine($"Images: {context.Images.Count}");
//            Console.WriteLine($"Images: {context.Colors.Count}");
//        }
//    }
//}
