using LibraryApp.Models;

namespace LibraryApp.Repositories
{
    public interface IBookRepository
    {
        List<Book> GetAllBooks();

        Book GetBookById(int id);
    }
}