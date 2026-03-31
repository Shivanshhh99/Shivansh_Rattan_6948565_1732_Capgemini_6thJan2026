namespace ProjectAlmanac
{
    class Almanac
    {
        public Dictionary<string, List<string>> projects =
            new Dictionary<string, List<string>>();

        public string AssignProject(string name, string project)
        {
            if (!projects.ContainsKey(name))
                projects[name] = new List<string>();

            projects[name].Add(project);
            return "Project Assigned";
        }

        public List<string> CurrentProjects(string name)
        {
            return projects.ContainsKey(name)
                ? projects[name]
                : new List<string>();
        }

        public string FinishProject(string name, string project)
        {
            if (projects.ContainsKey(name) &&
                projects[name].Contains(project))
            {
                projects[name].Remove(project);
                return "Project Finished";
            }
            return "Project Not Found";
        }
    }

    class Program
    {
        static void Main()
        {
            Almanac a = new Almanac();
            a.AssignProject("John", "AI");
            a.AssignProject("John", "ML");

            Console.WriteLine(string.Join(",", a.CurrentProjects("John")));
            Console.WriteLine(a.FinishProject("John", "AI"));
            Console.WriteLine(string.Join(",", a.CurrentProjects("John")));
        }
    }
}