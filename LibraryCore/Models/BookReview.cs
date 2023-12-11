using System;
using System.Collections.Generic;

namespace LibraryCore.Models
{
    public partial class BookReview
    {
        public int UserId { get; set; }
        public DateTime? Date { get; set; }
        public int? Rate { get; set; }
        public string? Comment { get; set; }
        public int BookId { get; set; }

        public virtual Book Book { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
