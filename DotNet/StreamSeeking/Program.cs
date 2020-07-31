using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace StreamSeeking
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileName = $"{Directory.GetCurrentDirectory()}\\test.txt";

            // Test 3
            var prop3 = JsonSeeker.ReadValue("prop3", fileName);
            if (prop3 == "{\"subProp1\":10,\"subProp2\":[\"alma\",\"körte\",\"szilva\"]}")
                Console.WriteLine("Great job");
            else
                Console.WriteLine("It sucks");
            /*******************************************/

            // Test 2
            var prop2 = JsonSeeker.ReadValue("prop2", fileName);
            if (prop2 == "Hello word")
                Console.WriteLine("Great job");
            else
                Console.WriteLine("It sucks");
            /*******************************************/

            // Test 1
            var prop1 = JsonSeeker.ReadValue("prop1", fileName);
            if(prop1 == "[1,2,3,4]")
                Console.WriteLine("Great job");
            else
                Console.WriteLine("It sucks");
            /*******************************************/

            // Test 4
            var prop4 = JsonSeeker.ReadValue("prop4", fileName);
            if (prop4 == "123")
                Console.WriteLine("Great job");
            else
                Console.WriteLine("It sucks");
            /*******************************************/
            
            // Console.WriteLine("found");
            // Console.WriteLine(temp.GetString(buffer));
            Console.ReadKey();
        }

 

    }
}
