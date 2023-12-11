using LibraryCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCore.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository BookRepository { get; }
        ICategoryRepository CategoryRepository { get; }

        IUserRepository UserRepository { get; }

        ILoanRepository LoanRepository { get; }

        IAppointmentRepository AppointmentRepository { get; }

        ICartRepository CartRepository { get; }

        IOrderRepository OrderRepository { get; }
        IOrderDetailRepository OrderDetailRepository { get; }
        IAuthorRepository AuthorRepository { get; }

        void SaveChange();

    }
}
