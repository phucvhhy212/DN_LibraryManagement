using LibraryCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCore.Repositories
{
    public class LoanRepository: GenericRepository<Loan>, ILoanRepository
    {
        public LoanRepository(LibraryDBContext context) : base(context)
        {

        }

        public int[] CountLoansByMonth()
        {

            var result = context.Loans
                .GroupBy(l => l.LoanDateFrom.Month)
                .OrderBy(g => g.Key)
                .Select(g => g.Count())
                .ToArray();
            
            return result;
        }
    }

}
