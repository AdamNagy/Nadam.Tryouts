using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multithreading
{
    internal class TestDataGenerator
    {
        private string _directory;
        private string _input;
        private string _output;

        public TestDataGenerator(string directiory, string input, string output)
        {
            _directory = directiory;
            _input = input;
            _output = output;
        }

        public void GenerateTestFile()
        {
            Console.WriteLine("Skipped lines");

            string? line = "";

            if (File.Exists($"{_directory}/{_output}"))
                File.Delete($"{_directory}/{_output}");

            using (var input = new StreamReader(new FileStream($"{_directory}/{_input}", FileMode.Open)))
            using (var output = new StreamWriter(new FileStream($"{_directory}/{_output}.txt", FileMode.CreateNew)))
            {
                line = input.ReadLine();
                while (line != null)
                {
                    if( line.StartsWith("http") )                    
                        output.WriteLine(line.Split(' ')[0]);                    
                    else
                        Console.WriteLine(line);

                    line = input.ReadLine();
                }
            }
        }
    }
}
