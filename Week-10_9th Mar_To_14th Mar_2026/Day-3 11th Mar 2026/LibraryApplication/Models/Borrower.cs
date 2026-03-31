using System;
using System.Collections.Generic;

namespace LibraryApplication.Models;

public partial class Borrower
{
    public int BorrowerId { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
