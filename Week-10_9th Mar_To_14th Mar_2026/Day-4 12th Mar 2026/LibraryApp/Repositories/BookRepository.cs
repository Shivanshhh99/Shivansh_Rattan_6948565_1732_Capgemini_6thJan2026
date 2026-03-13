using LibraryApp.Models;

namespace LibraryApp.Repositories
{
    public class BookRepository : IBookRepository
    {
        private List<Book> books = new List<Book>()
        {
            new Book{ Id = 1, Title="C# Basics", Author="John"},
            new Book{ Id = 2, Title="ASP.NET Core", Author="Smith"},
            new Book{ Id = 3, Title="MVC Design", Author="David"}
        };

        public List<Book> GetAllBooks()
        {
            return books;
        }

        public Book GetBookById(int id)
        {
            return books.FirstOrDefault(b => b.Id == id);
        }
    }
}