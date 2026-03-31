using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

class LogProcessingPipeline
{
    static int processedCount = 0;

    static void Main()
    {
        Console.WriteLine("=== Interactive Producer–Consumer Log Pipeline ===\n");

        Console.Write("Enter number of producers: ");
        int producerCount = int.Parse(Console.ReadLine());

        Console.Write("Enter number of consumers: ");
        int consumerCount = int.Parse(Console.ReadLine());

        Console.Write("Enter logs per producer: ");
        int logsPerProducer = int.Parse(Console.ReadLine());

        Console.Write("Enter buffer capacity: ");
        int capacity = int.Parse(Console.ReadLine());

        Console.Write("Simulate malformed logs? (yes/no): ");
        bool simulateMalformed = Console.ReadLine().Trim().ToLower() == "yes";

        // ---- Create bounded BlockingCollection ----
        using BlockingCollection<string> logBuffer = new BlockingCollection<string>(capacity);

        // ---- Start Consumers ----
        Task[] consumers = new Task[consumerCount];
        for (int i = 0; i < consumerCount; i++)
        {
            int consumerId = i + 1;
            consumers[i] = Task.Run(() =>
                ConsumeLogs(logBuffer, consumerId, simulateMalformed));
        }

        // ---- Start Producers ----
        Task[] producers = new Task[producerCount];
        for (int i = 0; i < producerCount; i++)
        {
            int producerId = i + 1;
            producers[i] = Task.Run(() =>
                ProduceLogs(logBuffer, producerId, logsPerProducer, simulateMalformed));
        }

        // Wait for producers to finish
        Task.WaitAll(producers);

        // Signal completion
        logBuffer.CompleteAdding();

        // Wait for consumers to finish
        Task.WaitAll(consumers);

        Console.WriteLine("\n--- Pipeline Complete ---");
        Console.WriteLine($"Total logs successfully processed: {processedCount}");
    }

    static void ProduceLogs(
        BlockingCollection<string> buffer,
        int producerId,
        int count,
        bool simulateMalformed)
    {
        for (int i = 1; i <= count; i++)
        {
            string log;
            if (simulateMalformed && i % 25 == 0)
            {
                log = "MALFORMED_LOG";
            }
            else
            {
                log = $"Producer {producerId} - Log {i}";
            }

            buffer.Add(log); // blocks if full
        }
    }

    static void ConsumeLogs(
        BlockingCollection<string> buffer,
        int consumerId,
        bool simulateMalformed)
    {
        foreach (var log in buffer.GetConsumingEnumerable())
        {
            try
            {
                // Simulate parsing
                if (simulateMalformed && log == "MALFORMED_LOG")
                    throw new FormatException("Invalid log format");

                // Simulate persistence
                Thread.Sleep(5);

                Interlocked.Increment(ref processedCount);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Consumer {consumerId} error: {ex.Message}");
            }
        }
    }
}
