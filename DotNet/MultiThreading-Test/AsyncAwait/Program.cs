using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AsyncAwait
{
    class Program
    {
        static void Main(string[] args)
        {
            var testFilePath = @"C:\Users\adna01\Documents\Learning\long_text.txt";
            var words = new List<string>();

            Stopwatch stopWatch = new Stopwatch();

            Console.WriteLine("Start reading");
            stopWatch.Start();
            foreach (var line in File.ReadLines(testFilePath))
            {
                words.AddRange(line.Split(' ').Select(p => p.Trim(',').Trim('.')));
            }
            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;

            Console.WriteLine("Stop reading");
            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);

            Console.WriteLine("RunTime " + elapsedTime);

            // CountWords(ref words);
            CountLetters(ref words);

            Console.ReadKey();
        }

        public static void CountWords(ref List<string> words)
        {
            var wordDict = new Dictionary<string, int>();
            (string word, int value) min = ("", Int32.MaxValue), max = ("", 0);
            int avarage = 0, wordCount = 0;

            Stopwatch stopWatch = new Stopwatch();

            Console.WriteLine("Start counting");
            stopWatch.Start();
            foreach (var word in words)
            {
                ++wordCount;

                if (wordDict.ContainsKey(word))
                    wordDict[word]++;
                else
                    wordDict.Add(word, 1);

                if (wordDict[word] < min.value)
                    min = (word, wordDict[word]);

                if (wordDict[word] > max.value)
                    max = (word, wordDict[word]);
            }

            avarage = wordCount / wordDict.Count();

            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;

            Console.WriteLine("Stop counting");
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);

            Console.WriteLine("RunTime " + elapsedTime);

            Console.WriteLine("\nAnalitics:");
            Console.WriteLine($"Avarage: {avarage}");
            Console.WriteLine($"Min: \"{min.word}\" {min.value} Max: \"{max.word}\" {max.value}");
            //foreach (var wordCount in wordDict)
            //{
            //    Console.WriteLine($"{wordCount.Key}: {wordCount.Value}");
            //}
            Console.WriteLine($"\"viverra\": {wordDict["viverra"]}");
        }

        public static void CountLetters(ref List<string> words)
        {
            var wordDict = new Dictionary<char, int>();
            (char word, int value) min = ('_', Int32.MaxValue), max = ('_', 0);
            int avarage = 0, wordCount = 0;

            Stopwatch stopWatch = new Stopwatch();

            Console.WriteLine("Start counting");
            stopWatch.Start();

            foreach (var word in words)
            {
                foreach (var letter in word)
                {
                    var lowerLetter = Char.ToLower(letter);
                    ++wordCount;

                    if (wordDict.ContainsKey(lowerLetter))
                        wordDict[lowerLetter]++;
                    else
                        wordDict.Add(lowerLetter, 1);

                    if (wordDict[lowerLetter] < min.value)
                        min = (lowerLetter, wordDict[lowerLetter]);

                    if (wordDict[lowerLetter] > max.value)
                        max = (lowerLetter, wordDict[lowerLetter]);
                }
            }

            wordDict.OrderBy(p => p.Key);

            avarage = wordCount / wordDict.Count();

            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;

            Console.WriteLine("Stop counting");
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);

            Console.WriteLine("RunTime " + elapsedTime);

            Console.WriteLine("\nAnalitics:");
            Console.WriteLine($"Avarage: {avarage}");
            Console.WriteLine($"Min: \"{min.word}\" {min.value} Max: \"{max.word}\" {max.value}");
            foreach (var character in wordDict)
            {
                Console.WriteLine($"{character.Key}: {character.Value}");
            }
            // Console.WriteLine($"\"viverra\": {wordDict["viverra"]}");
        }
    }
}
