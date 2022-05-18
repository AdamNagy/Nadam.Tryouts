using System;
using System.Threading;

namespace Exceptions
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo();
        }

        private static void Demo()
        {

            new Thread(Execute).Start();
        }

        static void Execute()
        {
            try
            {
                throw null;
            }
            catch (Exception ex)
            {

            }

        }
    }
}
