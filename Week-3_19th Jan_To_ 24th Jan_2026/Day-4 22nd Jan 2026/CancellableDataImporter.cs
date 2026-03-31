using System;
using System.Threading;
using System.Threading.Tasks;

class CancellableDataImporter
{
    static async Task Main()
    {
        Console.WriteLine("=== Cancellable Data Import ===\n");

        Console.Write("Enter total records to import: ");
        int totalRecords = int.Parse(Console.ReadLine());

        Console.Write("Enter timeout in milliseconds (0 for no timeout): ");
        int timeoutMs = int.Parse(Console.ReadLine());

        Console.Write("Enable manual cancel? (yes/no): ");
        bool enableManualCancel = Console.ReadLine().Trim().ToLower() == "yes";

        using CancellationTokenSource cts = new CancellationTokenSource();

        // Timeout cancellation
        if (timeoutMs > 0)
        {
            cts.CancelAfter(timeoutMs);
        }

        // Manual cancel via key press
        if (enableManualCancel)
        {
            Task.Run(() =>
            {
                Console.WriteLine("Press 'C' to cancel import...\n");
                while (true)
                {
                    if (Console.ReadKey(true).Key == ConsoleKey.C)
                    {
                        cts.Cancel(); // idempotent
                        break;
                    }
                }
            });
        }

        int importedCount = 0;

        try
        {
            importedCount = await ImportAsync(
                totalRecords,
                cts.Token,
                progress => Console.WriteLine($"Imported {progress} records...")
            );

            Console.WriteLine("\nImport completed successfully.");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("\nImport canceled.");
        }

        Console.WriteLine($"Total records imported: {importedCount}");
    }

    static async Task<int> ImportAsync(
        int totalRecords,
        CancellationToken token,
        Action<int> reportProgress)
    {
        int processed = 0;

        for (int i = 1; i <= totalRecords; i++)
        {
            token.ThrowIfCancellationRequested();

            // Simulate slow I/O
            await Task.Delay(50, token);

            processed++;

            // Report progress every 100 records
            if (processed % 100 == 0)
            {
                reportProgress(processed);
            }
        }

        return processed;
    }
}
