namespace What_is_an_Age
{
    
    class Person
    {
        private string name;
        private int age;

        public Person(string name, int age)
        {
            this.name = name;
            this.age = age;
        }

        public string Name => name;
        public int Age => age;
    }

    class StreamImplementation
    {
        public static int sumAge(List<Person> list)
        {
            return list.Where(p => p.Age > 50).Sum(p => p.Age);
        }

        public static List<string> printName(List<Person> list)
        {
            return list.Select(p => p.Name).ToList();
        }

        public static List<int> printAge(List<Person> list)
        {
            return list.Select(p => p.Age).ToList();
        }
    }

    class Program
    {
        static void Main()
        {
            List<Person> people = new List<Person>
        {
            new Person("A",60),
            new Person("B",40),
            new Person("C",70)
        };

            Console.WriteLine(StreamImplementation.sumAge(people));
            Console.WriteLine(string.Join(",", StreamImplementation.printName(people)));
            Console.WriteLine(string.Join(",", StreamImplementation.printAge(people)));
        }
    }
}