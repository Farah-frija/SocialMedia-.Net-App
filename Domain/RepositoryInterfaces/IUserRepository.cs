using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.RepositoryInterfaces
{
    public  interface IUserRepository
    {
        Task<User> GetByIdAsync(int id);
        Task UpdateAsync(User user);
    }
}
