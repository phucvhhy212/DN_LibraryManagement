using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryCore.Models
{
    public partial class User
    {
        public User()
        {
            Appointments = new HashSet<Appointment>();
            Blogs = new HashSet<Blog>();
            BookReviews = new HashSet<BookReview>();
            Fines = new HashSet<Fine>();
            Loans = new HashSet<Loan>();
            Reservations = new HashSet<Reservation>();
        }

        public int UserId { get; set; }
        public string Fullname { get; set; } = null!;
        public string Username { get; set; } = null!;
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        public string Mail { get; set; } = null!;
        public string UserRole { get; set; } = null!;
        public bool Status { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Blog> Blogs { get; set; }
        public virtual ICollection<BookReview> BookReviews { get; set; }
        public virtual ICollection<Fine> Fines { get; set; }
        public virtual ICollection<Loan> Loans { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
