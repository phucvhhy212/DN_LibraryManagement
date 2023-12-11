using System;
using System.Collections.Generic;

namespace LibraryCore.Models
{
    public partial class Fine
    {
        public int FineId { get; set; }
        public int UserId { get; set; }
        public int LoanId { get; set; }
        public DateTime FineDate { get; set; }
        public double FineAmount { get; set; }

        public virtual Loan Loan { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
