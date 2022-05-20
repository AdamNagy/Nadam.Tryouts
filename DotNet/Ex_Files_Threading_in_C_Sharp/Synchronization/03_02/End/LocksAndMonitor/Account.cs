using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LocksAndMonitor
{
    internal class Account
    {
        Object caztonLock = new Object();
        int balance;
        Random random = new Random();
        public Account(int initialBalance)
        {
            balance = initialBalance;
        }

        int Withdraw(int amount)
        {
            if (balance < 0)
            {
                throw new Exception("Not enough balance");
            }

            //Monitor.Enter(caztonLock);
            //try
            //{
            lock (caztonLock)
            {
                if (balance >= amount)
                {
                    Console.WriteLine("Amount drawn: " + amount);
                    balance = balance - amount;

                    return balance;
                }
                //}
                //finally
                //{
                //    Monitor.Exit(caztonLock);
                //}
                return 0;
            }
        }

        public void WithdrawRandomly()
        {
            for (int i = 0; i < 100; i++)
            {
                var balance = Withdraw(random.Next(2000, 5000));
                if (balance > 0)
                {
                    Console.WriteLine("Balance left" + balance);
                }
                else
                {
                    Console.WriteLine("Balance left" + balance);
                }
            }
        }
    }
}
