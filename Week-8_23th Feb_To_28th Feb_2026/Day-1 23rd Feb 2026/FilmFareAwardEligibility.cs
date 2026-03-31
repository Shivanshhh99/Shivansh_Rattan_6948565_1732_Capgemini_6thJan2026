namespace FilmFareAwardEligibility
{

    class MovieRatingException : Exception
    {
        public MovieRatingException(string msg) : base(msg) { }
    }

    class Rating
    {
        public int imdbRating;
        public int nominee;
    }

    class Validator
    {
        public static string CanBeConsideredForTheAward(Rating r)
        {
            if (r.imdbRating < 7)
                throw new MovieRatingException("Low IMDB Rating");

            if (r.nominee < 4)
                throw new MovieRatingException("Not Enough Nominations");

            return "Considered For Award";
        }

        public static string SendInvite(Rating r)
        {
            return "Invite Sent";
        }
    }

    class Program
    {
        static void Main()
        {
            Rating r = new Rating { imdbRating = 8, nominee = 5 };

            try
            {
                Console.WriteLine(Validator.CanBeConsideredForTheAward(r));
                Console.WriteLine(Validator.SendInvite(r));
            }
            catch (MovieRatingException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}