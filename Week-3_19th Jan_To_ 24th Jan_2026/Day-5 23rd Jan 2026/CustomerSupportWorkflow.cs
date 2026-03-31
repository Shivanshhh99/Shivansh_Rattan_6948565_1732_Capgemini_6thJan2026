using System;
using System.Collections.Generic;

class CustomerSupportWorkflow
{
    static void Main()
    {
        Queue<string> ticketQueue = new Queue<string>();

        Stack<string> actionHistory = new Stack<string>();

        Console.Write("Enter number of tickets: ");
        int ticketCount = int.Parse(Console.ReadLine());

        // Taking input
        for (int i = 1; i <= ticketCount; i++)
        {
            Console.Write($"Enter description for Ticket {i}: ");
            string ticket = Console.ReadLine();
            ticketQueue.Enqueue(ticket);
        }

        Console.WriteLine("\n--- Ticket Processing Started ---");

        int processed = 0;

        // Process only first three tickets
        while (ticketQueue.Count > 0 && processed < 3)
        {
            string currentTicket = ticketQueue.Dequeue();
            Console.WriteLine($"\nProcessing Ticket: {currentTicket}");

            Console.Write("Enter number of actions performed: ");
            int actionCount = int.Parse(Console.ReadLine());

            // Take actions from user
            for (int i = 1; i <= actionCount; i++)
            {
                Console.Write($"Enter action {i}: ");
                string action = Console.ReadLine();
                actionHistory.Push(action);
            }

            // Undo last action
            if (actionHistory.Count > 0)
            {
                string undoneAction = actionHistory.Pop();
                Console.WriteLine($"Undo last action: {undoneAction}");
            }

            // Clear history for next ticket
            actionHistory.Clear();
            processed++;
        }

        // Display remaining tickets
        Console.WriteLine("\n--- Remaining Tickets in Queue ---");
        if (ticketQueue.Count == 0)
        {
            Console.WriteLine("No remaining tickets.");
        }
        else
        {
            foreach (var ticket in ticketQueue)
            {
                Console.WriteLine(ticket);
            }
        }
    }
}
