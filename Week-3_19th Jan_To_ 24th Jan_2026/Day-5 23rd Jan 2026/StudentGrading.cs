using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        // Dictionary to store RollNo  Marks
        Dictionary<int, double> studentMarks = new Dictionary<int, double>();

        Console.Write("Enter number of students: ");
        int count = int.Parse(Console.ReadLine());

        for (int i = 0; i < count; i++)
        {
            Console.Write("\nEnter Roll Number: ");
            int roll = int.Parse(Console.ReadLine());

            Console.Write("Enter Marks: ");
            double marks = double.Parse(Console.ReadLine());

            studentMarks[roll] = marks;
        }

        // Func to calculate average marks
        Func<Dictionary<int, double>, double> calculateAverage =
            marks => marks.Values.Average();

        // Predicate to identify at-risk students (marks < 40)
        Predicate<double> isAtRisk = mark => mark < 40;

        Console.WriteLine("\n--- Student Details ---");
        foreach (var student in studentMarks)
        {
            Console.WriteLine($"Roll No: {student.Key}, Marks: {student.Value}, " +
                $"Status: {(isAtRisk(student.Value) ? "At Risk" : "Safe")}");
        }

        // Display average marks
        Console.WriteLine($"\nAverage Marks: {calculateAverage(studentMarks):F2}");

        // Update student marks
        Console.Write("\nEnter Roll Number to update marks: ");
        int updateRoll = int.Parse(Console.ReadLine());

        if (studentMarks.ContainsKey(updateRoll))
        {
            Console.Write("Enter new marks: ");
            double newMarks = double.Parse(Console.ReadLine());

            studentMarks[updateRoll] = newMarks;

            Console.WriteLine("\nMarks updated successfully!");
            Console.WriteLine($"Updated Status: {(isAtRisk(newMarks) ? "At Risk" : "Safe")}");
        }
        else
        {
            Console.WriteLine("Roll number not found.");
        }
    }
}
