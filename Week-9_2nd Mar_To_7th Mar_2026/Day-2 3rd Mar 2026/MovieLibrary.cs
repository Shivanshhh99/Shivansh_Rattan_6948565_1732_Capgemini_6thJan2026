using System;
using System.Collections.Generic;
using System.Linq;

public interface IFilm
{
    string Title { get; set; }
    string Director { get; set; }
    int Year { get; set; }
}

public class Film : IFilm
{
    public string Title { get; set; }
    public string Director { get; set; }
    public int Year { get; set; }

    public Film(string title, string director, int year)
    {
        Title = title;
        Director = director;
        Year = year;
    }
}

public interface IFilmLibrary
{
    void AddFilm(IFilm film);
    void RemoveFilm(string title);
    List<IFilm> GetFilms();
    List<IFilm> SearchFilms(string query);
    int GetTotalFilmCount();
}

public class FilmLibrary : IFilmLibrary
{
    private List<IFilm> _films = new List<IFilm>();

    public void AddFilm(IFilm film)
    {
        if (film != null)
            _films.Add(film);
    }

    public void RemoveFilm(string title)
    {
        var film = _films.FirstOrDefault(f =>
            f.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

        if (film != null)
            _films.Remove(film);
    }

    public List<IFilm> GetFilms()
    {
        return new List<IFilm>(_films);
    }

    public List<IFilm> SearchFilms(string query)
    {
        return _films.Where(f =>
            f.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
            f.Director.Contains(query, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public int GetTotalFilmCount()
    {
        return _films.Count;
    }
}

public class Program
{
    public static void Main()
    {
        FilmLibrary library = new FilmLibrary();

        Console.WriteLine("Enter number of films:");
        int n = int.Parse(Console.ReadLine());

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine("Enter title:");
            string title = Console.ReadLine();

            Console.WriteLine("Enter director:");
            string director = Console.ReadLine();

            Console.WriteLine("Enter year:");
            int year = int.Parse(Console.ReadLine());

            library.AddFilm(new Film(title, director, year));
        }

        Console.WriteLine("Enter search query:");
        string query = Console.ReadLine();

        var results = library.SearchFilms(query);

        Console.WriteLine("Search Results:");
        foreach (var film in results)
        {
            Console.WriteLine($"{film.Title} - {film.Director} ({film.Year})");
        }

        Console.WriteLine("Enter title to remove:");
        string removeTitle = Console.ReadLine();
        library.RemoveFilm(removeTitle);

        Console.WriteLine("Total films in library: " + library.GetTotalFilmCount());
    }
}