using System;
using System.Collections.Generic;
using System.Threading;

class OrderProcessingSystem
{
    // Shared counters
    private static int ProcessedCount = 0;
    private static int Revenue = 0;

    private static Queue<int> orders = new Queue<int>();
    private static object counterLock = new object();

    private static bool useLock = true;

    static void Main()
    {
        Console.WriteLine("=== E-Commerce Order Processing System ===\n");

        // ---- User Inputs ----
        Console.Write("Enter number of worker threads: ");
        int workerCount = int.Parse(Console.ReadLine());

        Console.Write("Enter total number of orders: ");
        int totalOrders = int.Parse(Console.ReadLine());

        Console.Write("Enter price per order (₹): ");
        int orderPrice = int.Parse(Console.ReadLine());

        Console.Write("Use lock for thread safety? (yes/no): ");
        useLock = Console.ReadLine().Trim().ToLower() == "yes";

        // Load orders
        for (int i = 0; i < totalOrders; i++)
        {
            orders.Enqueue(orderPrice);
        }

        Thread[] workers = new Thread[workerCount];

        Console.WriteLine("\nProcessing orders...\n");

        // Start worker threads
        for (int i = 0; i < workerCount; i++)
        {
            workers[i] = new Thread(ProcessOrders);
            workers[i].Start();
        }

        // Wait for all workers to complete
        foreach (Thread t in workers)
        {
            t.Join();
        }

        // Final output
        Console.WriteLine("\n=== Final Totals ===");
        Console.WriteLine($"Processed Orders : {ProcessedCount}");
        Console.WriteLine($"Total Revenue    : ₹{Revenue}");
    }

    static void ProcessOrders()
    {
        while (true)
        {
            int orderAmount;

            // Safely dequeue order
            lock (orders)
            {
                if (orders.Count == 0)
                    return;

                orderAmount = orders.Dequeue();
            }

            // Simulate work
            Thread.Sleep(1);

            // Update shared counters
            if (useLock)
            {
                lock (counterLock)
                {
                    ProcessedCount++;
                    Revenue += orderAmount;
                }
            }
            else
            {
                // Unsafe updates (race condition demo)
                ProcessedCount++;
                Revenue += orderAmount;
            }
        }
    }
}
