using System.Text;

namespace CollageManagementSystem
{

    internal class Program
    {
        class CollageManagement
        {
            Dictionary<string, Dictionary<string, int>> studentRecords =
                new Dictionary<string, Dictionary<string, int>>();

            Dictionary<string, LinkedList<KeyValuePair<string, int>>> subjectsStudentsOrder =
                new Dictionary<string, LinkedList<KeyValuePair<string, int>>>();

            Dictionary<string, Dictionary<string, int>> subjectsRecords =
                new Dictionary<string, Dictionary<string, int>>();

            public int AddStudent(string studentId, string subject, int marks)
            {
                if (!studentRecords.ContainsKey(studentId))
                    studentRecords[studentId] = new Dictionary<string, int>();

                if (!subjectsRecords.ContainsKey(subject))
                    subjectsRecords[subject] = new Dictionary<string, int>();

                if (!subjectsStudentsOrder.ContainsKey(subject))
                    subjectsStudentsOrder[subject] =
                        new LinkedList<KeyValuePair<string, int>>();

                if (studentRecords[studentId].ContainsKey(subject))
                {
                    if (marks > studentRecords[studentId][subject])
                    {
                        studentRecords[studentId][subject] = marks;
                        subjectsRecords[subject][studentId] = marks;

                        var node = subjectsStudentsOrder[subject].First;
                        while (node != null)
                        {
                            if (node.Value.Key == studentId)
                            {
                                node.Value =
                                    new KeyValuePair<string, int>(studentId, marks);
                                break;
                            }
                            node = node.Next;
                        }
                    }
                }
                else
                {
                    studentRecords[studentId][subject] = marks;
                    subjectsRecords[subject][studentId] = marks;

                    subjectsStudentsOrder[subject]
                        .AddLast(new KeyValuePair<string, int>(studentId, marks));
                }

                return 1;
            }

            public int RemoveStudent(string studentId)
            {
                if (!studentRecords.ContainsKey(studentId))
                    return 0;

                foreach (var sub in studentRecords[studentId].Keys)
                {
                    subjectsRecords[sub].Remove(studentId);

                    var node = subjectsStudentsOrder[sub].First;
                    while (node != null)
                    {
                        if (node.Value.Key == studentId)
                        {
                            subjectsStudentsOrder[sub].Remove(node);
                            break;
                        }
                        node = node.Next;
                    }
                }

                studentRecords.Remove(studentId);
                return 1;
            }

            public string TopStudent(string subject)
            {
                if (!subjectsRecords.ContainsKey(subject) ||
                    subjectsRecords[subject].Count == 0)
                    return "";

                int maxMarks = subjectsRecords[subject].Values.Max();

                StringBuilder sb = new StringBuilder();

                foreach (var pair in subjectsStudentsOrder[subject])
                {
                    if (pair.Value == maxMarks)
                    {
                        sb.AppendLine(pair.Key + " " + pair.Value);
                    }
                }

                return sb.ToString().Trim();
            }

            public string Result()
            {
                StringBuilder sb = new StringBuilder();

                foreach (var student in studentRecords)
                {
                    double avg = student.Value.Values.Average();
                    sb.AppendLine(student.Key + " " + avg.ToString("F2"));
                }

                return sb.ToString().Trim();
            }
        }

        public static void Main()
        {
            CollageManagement cm = new CollageManagement();

            cm.AddStudent("S1", "Math", 80);
            cm.AddStudent("S2", "Math", 90);
            cm.AddStudent("S3", "Math", 90);
            cm.AddStudent("S1", "Phy", 90);

            Console.WriteLine(cm.TopStudent("Math"));
            Console.WriteLine(cm.Result());

            cm.RemoveStudent("S1");
        }
    }
}