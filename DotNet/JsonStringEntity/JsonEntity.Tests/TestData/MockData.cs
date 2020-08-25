using System;
using System.Collections.Generic;

namespace TestData
{
    public static class MockData
    {
        public static string[] LONG_TEXTS = new[]
            {
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus viverra tortor vitae blandit varius. Duis dignissim, libero sit amet hendrerit ultricies, lectus purus elementum lectus, malesuada posuere dolor dui et ante. Quisque sodales vel sapien vel pulvinar. Nam nec nibh porttitor orci sodales facilisis. Duis aliquet molestie mauris, sed ultricies nisl. Ut diam ipsum, dictum vitae consectetur eget, convallis in ligula. Etiam sed laoreet eros. Phasellus consectetur, massa et finibus ullamcorper, est arcu condimentum nunc, quis ultricies purus leo eu sapien. Aenean fermentum, erat et tincidunt sagittis, leo quam imperdiet turpis, sed aliquet lacus velit nec nisl. Aenean et libero eget nisi tincidunt malesuada id mattis felis. Vivamus venenatis elit vel dui blandit posuere. Vestibulum et massa nisi. Proin ante ex, convallis id faucibus ac, fringilla eu quam. Donec in lectus eget libero luctus hendrerit. Sed tempor, dolor quis pellentesque euismod, libero mauris tincidunt turpis, maximus accumsan nibh nibh non elit. Nullam a egestas nunc, ut elementum libero.",
                "Proin tincidunt, ligula vel vulputate efficitur, diam ligula efficitur elit, nec lobortis neque enim vitae lectus. Proin quis maximus orci. Etiam quis rutrum mi. Suspendisse efficitur pharetra magna eget vehicula. Sed quis mauris ligula. Nullam ac pellentesque ex. Quisque tincidunt ultrices sapien sit amet ultricies. Sed ante ipsum, bibendum quis orci a, ullamcorper fermentum tellus. Quisque sollicitudin condimentum bibendum. Nulla non egestas arcu, id dapibus ligula. In cursus suscipit massa vel consectetur. Nam ac quam ante. Donec scelerisque ex sed congue venenatis.",
                "Nulla vitae ipsum a nisi blandit elementum. Aliquam id pellentesque quam. Vivamus imperdiet augue mi, in ultricies purus accumsan nec. Nullam sed metus neque. Nam pretium erat dui, et venenatis velit hendrerit eu. Suspendisse eu posuere urna. Praesent porta metus nec libero sollicitudin finibus. Aenean lacinia hendrerit mollis. Suspendisse eu purus varius, consectetur tortor vel, convallis elit. Phasellus et pretium metus. Suspendisse sit amet suscipit eros. Fusce ac arcu ac metus venenatis ultricies. Vivamus lobortis risus a quam consequat vehicula. Suspendisse ultricies efficitur nisi. Nullam vitae libero sed diam consequat auctor maximus id nisi.",
                "Phasellus non rutrum ex. In hac habitasse platea dictumst. Duis non bibendum eros, bibendum efficitur turpis. Nunc tristique risus nibh, id vulputate nibh rhoncus a. Ut dignissim blandit egestas. Nullam eu pharetra orci, vitae pulvinar est. Quisque tincidunt feugiat magna, nec imperdiet felis semper sed. Nulla egestas ipsum sed felis bibendum, sed ultricies ipsum posuere.",
                "Vivamus eu pulvinar justo. Phasellus nec euismod est. Sed at metus ultricies, consectetur risus ut, dictum ligula. Vivamus iaculis est non neque consectetur, ut ullamcorper magna molestie. Integer dictum lectus ultrices, pulvinar lectus tempor, feugiat magna. Aliquam vitae est sagittis, pretium nisi in, ultricies leo. Aliquam erat volutpat. Suspendisse at nisi quis neque ultricies vestibulum vel id tellus. Integer pretium tempor faucibus. Nunc eget porttitor augue. Nam tempus consectetur massa, a blandit enim viverra vel. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Curabitur at mi ultricies, molestie sapien vitae, congue arcu. Sed sodales massa at augue mattis ultrices. Proin ullamcorper, augue at porta iaculis, quam purus facilisis neque, a iaculis risus lacus ac metus. Ut diam sem, feugiat ac elit sit amet, dapibus hendrerit diam.",
            };

        public static string[] TEXTS = new[]
    {
                "Lorem ipsum dolor sit amet consectetur adipiscing elit.",
                "Proin tincidunt, ligula vel vulputate efficitur, diam ",
                "Nulla vitae ipsum a nisi blandit elementum.",
                "Phasellus non rutrum ex. In hac habitasse platea dictumst.",
                "Vivamus eu pulvinar justo. Phasellus nec euismod e",
            };

        public static int[] NUMBERS = new[]
        {
            1492, 1989, 2012, 2020, 1
        };

        public static IEnumerable<string> STRING_ARRAY1 = TEXTS[0].Split(' ');
        public static IEnumerable<string> STRING_ARRAY2 = TEXTS[1].Split(' ');
        public static IEnumerable<string> STRING_ARRAY3 = TEXTS[2].Split(' ');
        public static IEnumerable<string> STRING_ARRAY4 = TEXTS[3].Split(' ');
        public static IEnumerable<string> STRING_ARRAY5 = TEXTS[4].Split(' ');

        public static IEnumerable<int> NUMBERS_ARRAY1 = GenerateRandoms(5);
        public static IEnumerable<int> NUMBERS_ARRAY2 = GenerateRandoms(5);
        public static IEnumerable<int> NUMBERS_ARRAY3 = GenerateRandoms(5);
        public static IEnumerable<int> NUMBERS_ARRAY4 = GenerateRandoms(5);
        public static IEnumerable<int> NUMBERS_ARRAY5 = GenerateRandoms(5);

        public static IEnumerable<TestJsonModel2> COMPLEX_ARRAY= new List<TestJsonModel2>(3)
        {
            TestJsonModel2.GetDefault(),TestJsonModel2.GetDefault(),TestJsonModel2.GetDefault()
        };

        public static IEnumerable<int> GenerateRandoms(int n)
        {
            int Min = 0;
            int Max = 100;

            // this declares an integer array with 5 elements
            // and initializes all of them to their default value
            // which is zero
            int[] arrayOfInts = new int[n];

            Random randNum = new Random();
            for (int i = 0; i < arrayOfInts.Length; i++)
            {
                arrayOfInts[i] = randNum.Next(Min, Max);
            }

            return arrayOfInts;
        }
    }
}
