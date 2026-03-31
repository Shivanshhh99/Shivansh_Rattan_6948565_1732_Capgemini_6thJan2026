namespace StudentScholarshipScheme
{
    class Student
    {
        public string studentName;
        public int studentId;
        public int studentScore;
        public string scholarshipScheme;
    }

    class ScholarshipImpl
    {
        private List<Student> students = new List<Student>();

        public void AddStudent(Student s)
        {
            if (s.studentScore >= 90)
                s.scholarshipScheme = "Gold";
            else if (s.studentScore >= 75)
                s.scholarshipScheme = "Silver";
            else
                s.scholarshipScheme = "None";

            students.Add(s);
        }

        public List<Student> GetStudentDetails(string scheme)
        {
            return students
                .Where(s => s.scholarshipScheme == scheme)
                .ToList();
        }

        public void DeleteStudent(int id)
        {
            students.RemoveAll(s => s.studentId == id);
        }
    }

    class Program
    {
        static void Main()
        {
            ScholarshipImpl impl = new ScholarshipImpl();

            impl.AddStudent(new Student
            {
                studentName = "Ram",
                studentId = 1,
                studentScore = 92
            });

            var gold = impl.GetStudentDetails("Gold");
            Console.WriteLine(gold[0].studentName);

            impl.DeleteStudent(1);
        }
    }
}