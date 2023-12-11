using System;
using System.Collections.Generic;

namespace LibraryCore.Models
{
    public partial class Loan
    {
        public Loan()
        {
            Fines = new HashSet<Fine>();
        }

        public int LoanId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public DateTime LoanDateActual { get; set; }
        public DateTime LoanDateFrom { get; set; }
        public DateTime LoanDateTor { get; set; }
        public string Status { get; set; } = null!;

        public virtual Book Book { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Fine> Fines { get; set; }
    }
}
