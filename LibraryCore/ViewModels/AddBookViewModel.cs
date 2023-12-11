using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCore.ViewModels
{
    public class AddBookViewModel
    {
        public string Title { get; set; } = null!;
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }
        public string? Publisher { get; set; }
        public DateTime? PublicationDate { get; set; }
        public int Quantity { get; set; }
        public string? Image { get; set; }
        public double? Rate { get; set; }
        public bool? Status { get; set; }
        public string? Introduction { get; set; }
        public string? Description { get; set; }

        public double? Price { get; set; }

    }
}
