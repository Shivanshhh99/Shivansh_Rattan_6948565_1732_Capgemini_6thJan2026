namespace StudentsInClassroom
{

    class MainClass
    {
        public static List<string> changeOccurrence(List<string> a, string m, string n)
        {
            return a.Select(x => x == m ? n : x).ToList();
        }

        public static string listIndex(List<string> list)
        {
            return list.Count > 0 ? list[0] : null;
        }

        public static List<string> listAfter(List<string> a, string m, string n)
        {
            List<string> result = new List<string>();

            foreach (var item in a)
            {
                result.Add(item);
                if (item == m)
                    result.Add(n);
            }

            return result;
        }
    }

    class Program
    {
        static void Main()
        {
            List<string> students = new List<string> { "Ram", "John", "Ram" };

            var changed = MainClass.changeOccurrence(students, "Ram", "Ravi");
            Console.WriteLine(string.Join(",", changed));

            Console.WriteLine(MainClass.listIndex(students));

            var after = MainClass.listAfter(students, "Ram", "New");
            Console.WriteLine(string.Join(",", after));
        }
    }
}