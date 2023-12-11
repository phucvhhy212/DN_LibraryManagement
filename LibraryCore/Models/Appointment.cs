using System;
using System.Collections.Generic;

namespace LibraryCore.Models
{
    public partial class Appointment
    {
        public int AppointmentId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; } = null!;

        public virtual Book Book { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
