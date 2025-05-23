﻿// Create a scheduler that uses two threads.
using TaskSchedulerCustom;

LimitedConcurrencyLevelTaskScheduler customnScheduler = new LimitedConcurrencyLevelTaskScheduler(10);
List<Task> tasks = new List<Task>();

// Create a TaskFactory and pass it our custom scheduler.
TaskFactory factory = new TaskFactory(customnScheduler);
CancellationTokenSource cts = new CancellationTokenSource();

// Use our factory to run a set of tasks.
Object lockObj = new Object();
int outputItem = 0;

for (int tCtr = 0; tCtr <= 4; tCtr++)
{
    int iteration = tCtr;
    Task t = factory.StartNew(() => {
        for (int i = 0; i < 1000; i++)
        {
            lock (lockObj)
            {
                Console.Write("{0} in task t-{1} on thread {2}   ",
                              i, iteration, Thread.CurrentThread.ManagedThreadId);
                outputItem++;
                if (outputItem % 3 == 0)
                    Console.WriteLine();
            }
        }
    }, cts.Token);
    tasks.Add(t);
}

// Use it to run a second set of tasks.
for (int tCtr = 0; tCtr <= 4; tCtr++)
{
    int iteration = tCtr;
    Task t1 = factory.StartNew(() => {
        for (int outer = 0; outer <= 10; outer++)
        {
            for (int i = 0x21; i <= 0x7E; i++)
            {
                lock (lockObj)
                {
                    Console.Write("'{0}' in task t1-{1} on thread {2}   ",
                                  Convert.ToChar(i), iteration, Thread.CurrentThread.ManagedThreadId);
                    outputItem++;
                    if (outputItem % 3 == 0)
                        Console.WriteLine();
                }
            }
        }
    }, cts.Token);
    tasks.Add(t1);
}

// Wait for the tasks to complete before displaying a completion message.
Task.WaitAll(tasks.ToArray());
cts.Dispose();
Console.WriteLine("\n\nSuccessful completion.");

