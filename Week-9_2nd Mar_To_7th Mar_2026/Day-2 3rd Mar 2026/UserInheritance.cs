using System;

namespace Solution
{
    public enum Gender
    {
        Male,
        Female,
        Other
    }

    public abstract class User
    {
        protected string type;
        protected string name;
        protected Gender gender;
        protected int age;

        public User(string type, string name, Gender gender, int age)
        {
            this.type = type;
            this.name = name;
            this.gender = gender;
            this.age = age;
        }

        public string GetUserName() => name;
        public string GetUserType() => type;
        public int GetAge() => age;
        public Gender GetGender() => gender;
    }

    class Admin : User
    {
        public Admin(string name, Gender gender, int age)
            : base("Admin", name, gender, age) { }
    }

    class Moderator : User
    {
        public Moderator(string name, Gender gender, int age)
            : base("Moderator", name, gender, age) { }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("Enter Admin details (Name Gender Age):");
            string adminInput = Console.ReadLine();
            string[] adminData = adminInput.Split(' ');

            string adminName = adminData[0];
            Gender adminGender = (Gender)Enum.Parse(typeof(Gender), adminData[1], true);
            int adminAge = Convert.ToInt32(adminData[2]);

            Admin admin = new Admin(adminName, adminGender, adminAge);

            Console.WriteLine($"\nAdmin Info:");
            Console.WriteLine($"Type: {admin.GetUserType()}");
            Console.WriteLine($"Name: {admin.GetUserName()}");
            Console.WriteLine($"Age: {admin.GetAge()}");
            Console.WriteLine($"Gender: {admin.GetGender()}");

            Console.WriteLine("\nEnter Moderator details (Name Gender Age):");
            string modInput = Console.ReadLine();
            string[] modData = modInput.Split(' ');

            string modName = modData[0];
            Gender modGender = (Gender)Enum.Parse(typeof(Gender), modData[1], true);
            int modAge = Convert.ToInt32(modData[2]);

            Moderator moderator = new Moderator(modName, modGender, modAge);

            Console.WriteLine($"\nModerator Info:");
            Console.WriteLine($"Type: {moderator.GetUserType()}");
            Console.WriteLine($"Name: {moderator.GetUserName()}");
            Console.WriteLine($"Age: {moderator.GetAge()}");
            Console.WriteLine($"Gender: {moderator.GetGender()}");

            Console.WriteLine("\nPress Enter to exit...");
            Console.ReadLine();
        }
    }
}