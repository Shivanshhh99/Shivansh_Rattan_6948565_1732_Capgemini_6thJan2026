namespace CandidateValidationSystem
{

    class CriteriaMismatchException : Exception
    {
        public CriteriaMismatchException(string msg) : base(msg) { }
    }

    class Candidate
    {
        public string name;
        public int totalRating;
        public int totalContest;
    }

    class Validator
    {
        public static string Eligible(Candidate c)
        {
            if (c.totalRating < 1000)
                throw new CriteriaMismatchException("Low Rating");

            if (c.totalContest < 10)
                throw new CriteriaMismatchException("Less Contests");

            return "Eligible";
        }

        public static string SendInvite(Candidate c)
        {
            return "Invite Sent to " + c.name;
        }
    }

    class Program
    {
        static void Main()
        {
            Candidate c = new Candidate
            {
                name = "Alex",
                totalRating = 1200,
                totalContest = 15
            };

            try
            {
                Console.WriteLine(Validator.Eligible(c));
                Console.WriteLine(Validator.SendInvite(c));
            }
            catch (CriteriaMismatchException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}