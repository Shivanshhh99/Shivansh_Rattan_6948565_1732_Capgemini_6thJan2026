using System;
using System.Collections.Generic;

namespace LibraryApplication.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string? Title { get; set; }

    public string? Isbn { get; set; }

    public int? AuthorId { get; set; }

    public virtual Author? Author { get; set; }

    public virtual ICollection<Borrower> Borrowers { get; set; } = new List<Borrower>();
}
