using System;
using System.Collections.Generic;

namespace LibraryManagementSystem
{
    class Book
    {
        public int BookId;
        public string Title;
        public string Author;
        public bool IsIssued;

        public Book(int id, string title, string author)
        {
            BookId = id;
            Title = title;
            Author = author;
            IsIssued = false;
        }

        public void DisplayBook()
        {
            Console.WriteLine(BookId + " | " + Title + " | " + Author + " | " + (IsIssued ? "Issued" : "Available"));
        }
    }

    class Member
    {
        public int MemberId;
        public string Name;

        public Member(int id, string name)
        {
            MemberId = id;
            Name = name;
        }

        public void DisplayMember()
        {
            Console.WriteLine(MemberId + " | " + Name);
        }
    }

    class Library
    {
        List<Book> books = new List<Book>();
        List<Member> members = new List<Member>();

        public void AddBook()
        {
            Console.Write("Enter Book ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Enter Book Title: ");
            string title = Console.ReadLine();

            Console.Write("Enter Author Name: ");
            string author = Console.ReadLine();

            books.Add(new Book(id, title, author));

            Console.WriteLine("Book Added Successfully\n");
        }

        public void ViewBooks()
        {
            Console.WriteLine("\nBookID | Title | Author | Status");

            foreach (Book book in books)
            {
                book.DisplayBook();
            }
        }

        public void AddMember()
        {
            Console.Write("Enter Member ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Enter Member Name: ");
            string name = Console.ReadLine();

            members.Add(new Member(id, name));

            Console.WriteLine("Member Registered Successfully\n");
        }

        public void ViewMembers()
        {
            Console.WriteLine("\nMemberID | Name");

            foreach (Member m in members)
            {
                m.DisplayMember();
            }
        }

        public void IssueBook()
        {
            Console.Write("Enter Book ID to issue: ");
            int id = int.Parse(Console.ReadLine());

            foreach (Book book in books)
            {
                if (book.BookId == id)
                {
                    if (!book.IsIssued)
                    {
                        book.IsIssued = true;
                        Console.WriteLine("Book Issued Successfully");
                    }
                    else
                    {
                        Console.WriteLine("Book already issued");
                    }
                    return;
                }
            }

            Console.WriteLine("Book not found");
        }

        public void ReturnBook()
        {
            Console.Write("Enter Book ID to return: ");
            int id = int.Parse(Console.ReadLine());

            foreach (Book book in books)
            {
                if (book.BookId == id)
                {
                    if (book.IsIssued)
                    {
                        book.IsIssued = false;
                        Console.WriteLine("Book Returned Successfully");
                    }
                    else
                    {
                        Console.WriteLine("Book was not issued");
                    }
                    return;
                }
            }

            Console.WriteLine("Book not found");
        }

        public void SearchBook()
        {
            Console.Write("Enter Book Title: ");
            string title = Console.ReadLine();

            foreach (Book book in books)
            {
                if (book.Title.ToLower().Contains(title.ToLower()))
                {
                    book.DisplayBook();
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();
            int choice;

            do
            {
                Console.WriteLine("\n===== Library Management System =====");
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. View Books");
                Console.WriteLine("3. Register Member");
                Console.WriteLine("4. View Members");
                Console.WriteLine("5. Issue Book");
                Console.WriteLine("6. Return Book");
                Console.WriteLine("7. Search Book");
                Console.WriteLine("8. Exit");

                Console.Write("Enter choice: ");
                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        library.AddBook();
                        break;

                    case 2:
                        library.ViewBooks();
                        break;

                    case 3:
                        library.AddMember();
                        break;

                    case 4:
                        library.ViewMembers();
                        break;

                    case 5:
                        library.IssueBook();
                        break;

                    case 6:
                        library.ReturnBook();
                        break;

                    case 7:
                        library.SearchBook();
                        break;

                    case 8:
                        Console.WriteLine("Exiting...");
                        break;

                    default:
                        Console.WriteLine("Invalid Choice");
                        break;
                }

            } while (choice != 8);
        }
    }
}