namespace BookAnalysis
{

    class Book
    {
        public string bookName;
        public int bookCost;
    }

    class BookImplementation
    {
        public static string GetNameOfBooks(List<Book> list)
        {
            return string.Join(", ",
                list.Select(b => b.bookName + " - " + b.bookCost));
        }

        public static int SumCostOfAllBooks(List<Book> list)
        {
            return list.Sum(b => b.bookCost);
        }

        public static int GetMax(List<Book> list)
        {
            return list.Max(b => b.bookCost);
        }
    }

    class Program
    {
        static void Main()
        {
            List<Book> books = new List<Book>
        {
            new Book{bookName="C#", bookCost=500},
            new Book{bookName="Java", bookCost=700}
        };

            Console.WriteLine(BookImplementation.GetNameOfBooks(books));
            Console.WriteLine(BookImplementation.SumCostOfAllBooks(books));
            Console.WriteLine(BookImplementation.GetMax(books));
        }
    }
}