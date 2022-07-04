using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevGuideToCollections;

namespace Driver
{
    class Program
    {

        #region Helpers

        static string ArrayToString(Array array)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[");
            if (array.Length > 0)
            {
                sb.Append(array.GetValue(0));
            }
            for (int i = 1; i < array.Length; ++i)
            {
                sb.AppendFormat(",{0}", array.GetValue(i));
            }
            sb.Append("]");

            return sb.ToString();
        }

        #endregion

        #region Lessons

        #region Lesson4A Helpers

        static void WriteSeperator(int line)
        {
            Console.SetCursorPosition(0, line);
            for (int i = 0; i < Console.WindowWidth - 1; ++i)
            {
                Console.Write("-");
            }
        }

        static void WriteNextRequest(Request request, ref int nextRow)
        {
            Console.SetCursorPosition(0, nextRow);
            string line = string.Format("{0}: User request at {1}", request.RequestSong.Name, request.RequestTime);
            Console.WriteLine(line.PadRight(Console.WindowWidth - 1, ' '));
            nextRow = Math.Min(Console.CursorTop, Console.BufferHeight - 4);
        }

        static void WriteCurrentPlaying(Request currentRequest, int playRow, TimeSpan currentTime)
        {
            string line;

            Console.SetCursorPosition(0, playRow);
            line = string.Format("{0}: Playing {1}", currentRequest.RequestSong.Name, currentTime - currentRequest.FinishTime);
            Console.Write(line.PadRight(Console.WindowWidth, ' '));


            Console.SetCursorPosition(0, playRow + 1);

            int count = 0;
            for (TimeSpan curr = currentRequest.StartTime; curr < currentRequest.FinishTime; curr += TimeSpan.FromSeconds(30))
            {
                if (curr < currentTime)
                {
                    if (curr + TimeSpan.FromSeconds(30) > currentTime)
                    {
                        switch ((int)((currentTime - curr).TotalSeconds / 8) % 4)
                        {
                            case 1:
                                Console.Write("-");
                                break;
                            case 2:
                                Console.Write("\\");
                                break;
                            case 3:
                                Console.Write("|");
                                break;
                            case 0:
                            default:
                                Console.Write("/");
                                break;
                        }
                    }
                    else
                    {
                        Console.Write("*");
                    }
                }
                else
                {
                    Console.Write(".");
                }
                ++count;
            }

            // Songs can be up to four minutes or 8 (4min / 30sec) "*" or "."
            for (int i = count; i <= 9; ++i)
            {
                Console.Write(" ");
            }
        }

        static void PrintMenu()
        {
            Console.WriteLine("1. Add Name");
            Console.WriteLine("2. Lookup Number");
            Console.WriteLine("3. Show Phonebook");
            Console.WriteLine("4. Exit");
            Console.Write("> ");
        }

        static bool s_firstAdvance = true;

        static void AdvanceLine()
        {
            Console.SetCursorPosition(0, Console.BufferHeight - 1);
            if (!s_firstAdvance)
            {
                Console.WriteLine("");
            }
            else
            {
                s_firstAdvance = false;
            }
        }

        #endregion

        static void Lesson4A()
        {
            Request nowPlaying = null;
            QueuedArray<Request> requests = new QueuedArray<Request>();
            TimeSpan nextCallIn = TimeSpan.Zero;
            TimeSpan currentTime = TimeSpan.Zero;
            TimeSpan step = TimeSpan.FromSeconds(1);
            Random rnd = new Random();
            double playRate = 100;
            int nextRow = 0;
            int playRow = Console.WindowHeight - 2;
            ArrayEx<Song> availableSongs = new ArrayEx<Song>(20);

            Console.Clear();

            // Create a random list of songs
            for (int i = 0; i < 20; ++i)
            {
                availableSongs.Add(new Song()
                {
                    Name = string.Format("Song #{0}", i + 1),
                    // Each song is from 3 minutes to 4 minutes
                    Duration = TimeSpan.FromSeconds(3 * 60 + rnd.Next(121))
                });
            }

            Console.BufferHeight = 30;

            WriteSeperator(Console.WindowHeight - 3);

            while (!Console.KeyAvailable)
            {
                if (nowPlaying != null)
                {
                    // Check to see if the current request is finish
                    if (currentTime >= nowPlaying.FinishTime)
                    {
                        nowPlaying = null;
                    }
                }
                else
                {
                    // Check to see if there are any request waiting
                    if (requests.Count > 0)
                    {
                        nowPlaying = requests.Pop();
                        nowPlaying.StartTime = currentTime;
                    }
                }

                if (nowPlaying != null)
                {
                    WriteCurrentPlaying(nowPlaying, playRow, currentTime);
                }

                if (currentTime >= nextCallIn)
                {
                    // Pretend that someone has called in a request
                    Request request = new Request()
                    {
                        RequestTime = currentTime,
                        RequestSong = availableSongs[rnd.Next(availableSongs.Count - 1)]
                    };

                    WriteNextRequest(request, ref nextRow);

                    if (nextRow >= Console.WindowHeight - 2)
                    {

                        playRow = Math.Min(Console.CursorTop + 1, Console.BufferHeight - 2);

                        if (playRow == Console.BufferHeight - 2)
                        {
                            // Shifts all of the text up
                            AdvanceLine();

                            ++nextRow;
                        }

                        WriteSeperator(playRow - 1);
                    }

                    // Add the next request to the queue
                    requests.Push(request);

                    // Simulate someone calling in from 0 to 5 minutes from now
                    nextCallIn = currentTime + TimeSpan.FromSeconds(rnd.Next(5 * 60));
                }

                // Simulate actual time
                System.Threading.Thread.Sleep(Math.Max(0, (int)(step.TotalMilliseconds / playRate)));

                currentTime += step;
            }
        }

        static void Lesson5A()
        {
            Request nowPlaying = null;
            CircularBuffer<Request> requests = new CircularBuffer<Request>(10);
            TimeSpan nextCallIn = TimeSpan.Zero;
            TimeSpan currentTime = TimeSpan.Zero;
            TimeSpan step = TimeSpan.FromSeconds(1);
            Random rnd = new Random();
            double playRate = 100;
            int nextRow = 0;
            int playRow = Console.WindowHeight - 2;
            ArrayEx<Song> availableSongs = new ArrayEx<Song>(20);

            Console.Clear();

            // Create a random list of songs
            for (int i = 0; i < 20; ++i)
            {
                availableSongs.Add(new Song()
                {
                    Name = string.Format("Song #{0}", i + 1),
                    // Each song is from 3 minutes to 4 minutes
                    Duration = TimeSpan.FromSeconds(3 * 60 + rnd.Next(121))
                });
            }

            Console.BufferHeight = 30;

            WriteSeperator(Console.WindowHeight - 3);

            while (!Console.KeyAvailable)
            {
                if (nowPlaying != null)
                {
                    // Check to see if the current request is finish
                    if (currentTime >= nowPlaying.FinishTime)
                    {
                        nowPlaying = null;
                    }
                }
                else
                {
                    // Check to see if there are any request waiting
                    if (requests.Count > 0)
                    {
                        nowPlaying = requests.Pop();
                        nowPlaying.StartTime = currentTime;
                    }
                }

                if (nowPlaying != null)
                {
                    WriteCurrentPlaying(nowPlaying, playRow, currentTime);
                }

                if (currentTime >= nextCallIn)
                {
                    // Pretend that someone has called in a request
                    Request request = new Request()
                    {
                        RequestTime = currentTime,
                        RequestSong = availableSongs[rnd.Next(availableSongs.Count - 1)]
                    };

                    WriteNextRequest(request, ref nextRow);

                    if (nextRow >= Console.WindowHeight - 2)
                    {

                        playRow = Math.Min(Console.CursorTop + 1, Console.BufferHeight - 2);

                        if (playRow == Console.BufferHeight - 2)
                        {
                            // Shifts all of the text up
                            AdvanceLine();

                            ++nextRow;
                        }

                        WriteSeperator(playRow - 1);
                    }

                    // Add the next request to the queue
                    requests.Push(request);

                    // Simulate someone calling in from 0 to 5 minutes from now
                    nextCallIn = currentTime + TimeSpan.FromSeconds(rnd.Next(5 * 60));
                }

                // Simulate actual time
                System.Threading.Thread.Sleep(Math.Max(0, (int)(step.TotalMilliseconds / playRate)));

                currentTime += step;
            }
        }

        static void Lesson6A()
        {
            StackedArray<Plate> cleanPlates = new StackedArray<Plate>();
            SingleLinkedList<Plate> usingPlates = new SingleLinkedList<Plate>();
            QueuedArray<Plate> dirtyPlates = new QueuedArray<Plate>();
            Random rnd = new Random();
            TimeSpan currentTime = TimeSpan.Zero;
            TimeSpan step = TimeSpan.FromSeconds(1);
            double playRate = 100.0;
            TimeSpan nextCustomer = TimeSpan.Zero;
            bool hasWarned = false;

            for (int i = 0; i < 20; ++i)
            {
                cleanPlates.Push(new Plate() { Number = i });
            }

            while (!Console.KeyAvailable)
            {
                // Simulates the person eating and carrying the plate to the conveyer belt to be cleaned
                if (usingPlates.Count > 0)
                {
                    SingleLinkedListNode<Plate> node = usingPlates.Head;
                    while (node != null)
                    {
                        if (currentTime >= node.Data.NextOperation)
                        {
                            Plate plate = node.Data;

                            Console.WriteLine("Plate {0} is being taken to the cleaners", plate.Number);

                            // Takes 20 seconds to clean a plate
                            plate.NextOperation = currentTime + TimeSpan.FromSeconds(20);

                            // Add the plate to the conveyer belt to be cleaned
                            dirtyPlates.Push(plate);
                            SingleLinkedListNode<Plate> tmp = node.Next;
                            usingPlates.Remove(node);
                            node = tmp;
                        }
                        else
                        {
                            node = node.Next;
                        }
                    }
                }

                // Simulates plates being cleaned
                if (dirtyPlates.Count > 0)
                {
                    Plate plate = dirtyPlates.Peek();

                    if (currentTime >= plate.NextOperation)
                    {
                        Console.WriteLine("Plate {0} has been cleaned", plate.Number);
                        dirtyPlates.Pop();
                        cleanPlates.Push(plate);
                    }
                }

                // Simulates the person standing in line
                if (currentTime >= nextCustomer)
                {
                    if (!cleanPlates.IsEmpty)
                    {
                        Plate plate = cleanPlates.Pop();

                        // It can take up to 3 minutes for the customer to eat
                        plate.NextOperation = currentTime + TimeSpan.FromSeconds(rnd.Next(3 * 60));
                        usingPlates.AddToEnd(plate);

                        // Next customer can take up to 30 seconds
                        nextCustomer = currentTime + TimeSpan.FromSeconds(rnd.Next(30));

                        Console.WriteLine("Plate {0} has been taken", plate.Number);


                        hasWarned = false;
                    }
                    else
                    {
                        if (!hasWarned)
                        {
                            Console.WriteLine("Waiting for plates to be cleaned!");
                            hasWarned = true;
                        }
                    }
                }


                // Simulates actual time
                System.Threading.Thread.Sleep(Math.Max(0, (int)(step.TotalMilliseconds / playRate)));

                currentTime += step;
            }
        }

        #endregion

        static void PrintOptions()
        {
            Console.WriteLine("1. Lesson 4A");
            Console.WriteLine("2. Lesson 5A");
            Console.WriteLine("3. Lesson 6A");
            Console.WriteLine("4. Test Collections");
            Console.WriteLine("5. Exit");
            Console.Write("> ");
        }
        
        static void Main(string[] args)
        {
            string option = "";

            for (; ; )
            {
                PrintOptions();

                option = Console.ReadLine().Trim();

                Console.WriteLine();
                if (option == "1")
                {
                    Lesson4A();
                }
                else if (option == "2")
                {
                    Lesson5A();
                }
                else if (option == "3")
                {
                    Lesson6A();
                }
                else if (option == "4")
                {
                    Console.WriteLine("***Testing Collections***");
                    UnitTests.RunTests();
                    Console.WriteLine("Completed");
                }
                else if (option == "5")
                {
                    break;
                }
                Console.WriteLine();
            }
        }
    }
}
