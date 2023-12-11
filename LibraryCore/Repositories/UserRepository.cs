using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryCore.Models;

namespace LibraryCore.Repositories
{
    internal class UserRepository:GenericRepository<User>,IUserRepository

    {
        public UserRepository(LibraryDBContext context) : base(context)
        {
            
        }
    }
}
