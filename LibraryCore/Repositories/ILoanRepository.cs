using LibraryCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCore.Repositories
{
    public interface ILoanRepository: IGenericRepository<Loan>
    {
        int[] CountLoansByMonth();
    }
}
