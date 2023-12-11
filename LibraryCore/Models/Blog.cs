using System;
using System.Collections.Generic;

namespace LibraryCore.Models
{
    public partial class Blog
    {
        public int BlogId { get; set; }
        public string Title { get; set; } = null!;
        public string Detail { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string Image { get; set; } = null!;
        public int UserId { get; set; }
        public string Status { get; set; } = null!;

        public virtual User User { get; set; } = null!;
    }
}
